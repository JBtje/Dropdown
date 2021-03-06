﻿namespace MVVM
{
    using System;

    //https://gist.github.com/ReedCopsey/ba11d8d691f27ca2710f

    /// <summary>
    /// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <remarks>
    /// The constructor deliberately prevent the use of value types.
    /// Because ICommand takes an object, having a value type for T would cause unexpected behavior when CanExecute(null) is called during XAML initialization for command bindings.
    /// Using default(T) was considered and rejected as a solution because the implementor would not be able to distinguish between a valid and defaulted values.
    /// <para/>
    /// Instead, callers should support a value type by using a nullable value type and checking the HasValue property before using the Value property.
    /// <example>
    ///     <code>
    /// public MyClass()
    /// {
    ///     this.submitCommand = new DelegateCommand&lt;int?&gt;(this.Submit, this.CanSubmit);
    /// }
    ///
    /// private bool CanSubmit(int? customerId)
    /// {
    ///     return (customerId.HasValue &amp;&amp; customers.Contains(customerId.Value));
    /// }
    ///     </code>
    /// </example>
    /// </remarks>
    public class DelegateCommand<T> : DelegateCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <param name="useCommandManager">if set to <c>true</c> use the command manager instead of our own event process for CanExecuteChanged Tracking.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public DelegateCommand(Action<T> executeMethod, bool useCommandManager = false)
            : this(executeMethod, o => true, useCommandManager)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command.  This can be null.</param>
        /// <param name="useCommandManager">if set to <c>true</c> use the command manager instead of our own event process for CanExecuteChanged Tracking.</param>
        /// <exception cref="ArgumentNullException">When both <paramref name="executeMethod"/> and <paramref name="canExecuteMethod"/> ar <see langword="null" />.</exception>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, bool useCommandManager = false)
            : base((o) => executeMethod((T)o), o => canExecuteMethod((T)o), useCommandManager)
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), "Execute method cannot be null");
            }

            Type genericType = typeof(T);

            // DelegateCommand allows object or Nullable<>.
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericType.IsValueType)
            {
                if ((!genericType.IsGenericType) || (!typeof(Nullable<>).IsAssignableFrom(genericType.GetGenericTypeDefinition())))
                {
                    throw new InvalidCastException("Only reference or nullable types are supported.");
                }
            }
        }

        /// <summary>
        /// Determines if the command can execute by invoked the <see cref="Func{T,Bool}"/> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command to determine if it can execute.</param>
        /// <returns>
        /// <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return this.CanExecute(parameter);
        }

        /// <summary>
        /// Executes the command and invokes the <see cref="Action{T}"/> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void Execute(T parameter)
        {
            this.Execute(parameter);
        }
    }
}