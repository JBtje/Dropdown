namespace MVVM
{
    using System;

    //https://gist.github.com/ReedCopsey/ba11d8d691f27ca2710f

    /// <summary>
    /// An <see cref="ICommand"/> whose delegates do not take any parameters for <see cref="Execute"/> and <see cref="CanExecute"/>.
    /// </summary>
    public class ActionCommand : DelegateCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class with the <see cref="Action"/> to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="useCommandManager">if set to <c>true</c> use the command manager instead of our own event process for CanExecuteChanged Tracking.</param>
        public ActionCommand(Action executeMethod, bool useCommandManager = false)
            : this(executeMethod, () => true, useCommandManager)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class with the <see cref="Action"/> to invoke on execution
        /// and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
        /// <param name="useCommandManager">if set to <c>true</c> use the command manager instead of our own event process for CanExecuteChanged Tracking.</param>
        public ActionCommand(Action executeMethod, Func<bool> canExecuteMethod, bool useCommandManager = false)
            : base(o => executeMethod(), o => canExecuteMethod(), useCommandManager)
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), "Execute method cannot be null");
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            this.Execute(null);
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the command can execute,otherwise returns <see langword="false"/>.</returns>
        public bool CanExecute()
        {
            return this.CanExecute(null);
        }
    }
}