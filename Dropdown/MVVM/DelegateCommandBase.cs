namespace MVVM
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    //https://gist.github.com/ReedCopsey/ba11d8d691f27ca2710f

    /// <summary>
    /// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
    /// </summary>
    public abstract class DelegateCommandBase : IDelegateCommand
    {
        /// <summary>
        /// The execute method
        /// </summary>
        private readonly Action<object> executeMethod;

        /// <summary>
        /// The can execute method
        /// </summary>
        private readonly Func<object, bool> canExecuteMethod;

        /// <summary>
        /// Flag whether to use the command manager, or our own event handling
        /// </summary>
        private readonly bool useCommandManager;

        /// <summary>
        /// Initializes a new instance of the DelegateCommandBase class, specifying both the execute action and the can execute function.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to execute when <see cref="ICommand.Execute"/> is invoked.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{Object,Bool}"/> to invoked when <see cref="ICommand.CanExecute"/> is invoked.</param>
        /// <param name="useCommandManager">if set to <c>true</c> [use command manager].</param>
        protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod, bool useCommandManager)
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), "Execute method cannot be null");
            }

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
            this.useCommandManager = useCommandManager;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute. You must keep a hard
        /// reference to the handler to avoid garbage collection and unexpected results.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (useCommandManager)
                {
                    CommandManager.RequerySuggested += value;
                }
                else
                {
                    CanExecuteChangedWeakEventManager.AddHandler(this, value);
                }
            }

            remove
            {
                if (useCommandManager)
                {
                    CommandManager.RequerySuggested -= value;
                }
                else
                {
                    CanExecuteChangedWeakEventManager.RemoveHandler(this, value);
                }
            }
        }

        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        /// Raises <see cref="DelegateCommandBase.CanExecuteChanged"/> on the UI thread so every command invoker
        /// can requery to check if the command can execute.
        /// <remarks>Note that this will trigger the execution of <see cref="DelegateCommandBase.CanExecute"/> once for each invoker.</remarks>
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/> on the UI thread so every
        /// command invoker can requery <see cref="ICommand.CanExecute"/> to check if the
        /// ICommand can execute.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            if (useCommandManager)
            {
                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                var handler = this.CanExecuteChangedInternal;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Executes the command with the provided parameter by invoking the <see cref="Action{Object}"/> supplied during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        protected void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        /// <summary>
        /// Determines if the command can execute with the provided parameter by invoing the <see cref="Func{Object,Bool}"/> supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>Returns <see langword="true"/> if the command can execute.  <see langword="False"/> otherwise.</returns>
        protected bool CanExecute(object parameter)
        {
            return canExecuteMethod == null || canExecuteMethod(parameter);
        }

        /// <summary>
        /// Internal WeakEventManager to handle the events in a weak way
        /// </summary>
        private class CanExecuteChangedWeakEventManager : WeakEventManager
        {
            private CanExecuteChangedWeakEventManager()
            {
            }

            /// <summary>
            /// Get the event manager for the current thread.
            /// </summary>
            private static CanExecuteChangedWeakEventManager CurrentManager
            {
                get
                {
                    Type managerType = typeof(CanExecuteChangedWeakEventManager);
                    CanExecuteChangedWeakEventManager manager =
                        (CanExecuteChangedWeakEventManager)GetCurrentManager(managerType);

                    // at first use, create and register a new manager
                    if (manager == null)
                    {
                        manager = new CanExecuteChangedWeakEventManager();
                        WeakEventManager.SetCurrentManager(managerType, manager);
                    }

                    return manager;
                }
            }

            /// <summary>
            /// Add a handler for the given source's event.
            /// </summary>
            public static void AddHandler(DelegateCommandBase source, EventHandler handler)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (handler == null)
                {
                    throw new ArgumentNullException(nameof(handler));
                }

                CurrentManager.ProtectedAddHandler(source, handler);
            }

            /// <summary>
            /// Remove a handler for the given source's event.
            /// </summary>
            public static void RemoveHandler(DelegateCommandBase source, EventHandler handler)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (handler == null)
                {
                    throw new ArgumentNullException(nameof(handler));
                }

                CurrentManager.ProtectedRemoveHandler(source, handler);
            }

            /// <summary>
            /// Return a new list to hold listeners to the event.
            /// </summary>
            protected override ListenerList NewListenerList()
            {
                return new ListenerList();
            }

            /// <summary>
            /// Listen to the given source for the event.
            /// </summary>
            protected override void StartListening(object source)
            {
                DelegateCommandBase typedSource = (DelegateCommandBase)source;
                typedSource.CanExecuteChangedInternal += this.OnCanExecuteChangedInternal;
            }

            /// <summary>
            /// Stop listening to the given source for the event.
            /// </summary>
            protected override void StopListening(object source)
            {
                DelegateCommandBase typedSource = (DelegateCommandBase)source;
                typedSource.CanExecuteChangedInternal -= this.OnCanExecuteChangedInternal;
            }

            /// <summary>
            /// Event handler for the SomeEvent event.
            /// </summary>
            private void OnCanExecuteChangedInternal(object sender, EventArgs e)
            {
                DeliverEvent(sender, e);
            }
        }
    }
}