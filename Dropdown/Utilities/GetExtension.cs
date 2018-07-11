namespace Dropdown
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;
    using Ninject;

    public class GetExtension : MarkupExtension
    {
        private static readonly DependencyObject DependencyObject = new DependencyObject();

        public GetExtension()
        {
        }

        public GetExtension(Type type)
            : this(type, false)
        {
        }

        public GetExtension(Type type, bool isDesignTimeCreatable)
        {
            this.Type = type;
            this.IsDesignTimeCreatable = isDesignTimeCreatable;
        }

        [ConstructorArgument("type")]
        public Type Type { get; set; }

        [ConstructorArgument("isDesignTimeCreatable")]
        public bool IsDesignTimeCreatable { get; set; }

        private static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(DependencyObject);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type == null)
            {
                return null;
            }

            if (IsInDesignMode)
            {
                return IsDesignTimeCreatable ? App.Kernel?.Get(Type) : null;
            }

            return App.Kernel.Get(Type);
        }
    }
}
