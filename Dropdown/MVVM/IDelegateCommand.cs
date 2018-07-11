namespace MVVM
{
    using System.Windows.Input;

    /// <summary>
    /// Interface to extend a command and allow you to handle raising CanExecuteChanged
    /// </summary>
    public interface IDelegateCommand : ICommand
    {
        // https://gist.github.com/ReedCopsey/ba11d8d691f27ca2710f

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/>
        /// <remarks>Note that this will trigger the execution of <see cref="ICommand.CanExecute"/> once for each invoker.</remarks>
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}