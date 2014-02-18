namespace Thinknet.MVVM.ViewModel
{
    using System;

    /// <summary>
    /// Formatting view model, can be used for customized formatting of any models.
    /// </summary>
    /// <typeparam name="T">The type of the containing model.</typeparam>
    public class FormattingViewModel<T> : ViewModel<T>
    {
        private readonly Func<T, string> _formatter; 

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattingViewModel{T}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="formatter">Formatter used by Text and ToString().</param>
        public FormattingViewModel(T model, Func<T, string> formatter)
        {
            Model = model;
            _formatter = formatter;
        }

        /// <summary>
        /// Gets the formatted text.
        /// </summary>
        public string Text
        {
            get
            {
                if (_formatter != null)
                {
                    return _formatter.Invoke(Model);
                }

                return base.ToString();                   
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Text;
        }
    }
}
