using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The RotarySelectorItem displays Menu Item on the RotarySelectorPage.
    /// </summary>
    public class RotarySelectorItem : BaseMenuItem
    {
        /// <summary>
        /// Identifies the Text bound property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RotarySelectorItem));
        /// <summary>
        /// Identifies the SubText bound property.
        /// </summary>
        public static readonly BindableProperty SubTextProperty = BindableProperty.Create(nameof(SubText), typeof(string), typeof(RotarySelectorItem));

        /// <summary>
        /// Identifies the Icon bound property
        /// </summary>
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(FileImageSource), typeof(RotarySelectorItem));
        /// <summary>
        /// Identifies the IconAlphaBlendingColor bound property
        /// </summary>
        public static readonly BindableProperty IconAlphaBlendingColorProperty = BindableProperty.Create(nameof(IconAlphaBlendingColor), typeof(Color), typeof(RotarySelectorItem), Color.White);

        /// <summary>
        /// Identifies the ItemBackgroundImage bound property
        /// </summary>
        public static readonly BindableProperty ItemBackgroundImageProperty = BindableProperty.Create(nameof(ItemBackgroundImage), typeof(FileImageSource), typeof(RotarySelectorItem));
        /// <summary>
        /// Identifies the ItemIcon bound property
        /// </summary>
        public static readonly BindableProperty ItemIconProperty = BindableProperty.Create(nameof(ItemIcon), typeof(FileImageSource), typeof(RotarySelectorItem));

        /// <summary>
        /// Identifies the Command bound property
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RotarySelectorItem));
        /// <summary>
        /// Identifies the CommandParameter bound property
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RotarySelectorItem));

        /// <summary>
        ///  Gets or sets the command that is run when the item is clicked.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter that is passed to the command.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// The text of the item.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The sub-text of the item.
        /// </summary>
        public string SubText
        {
            get => (string)GetValue(SubTextProperty);
            set => SetValue(SubTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the icon for the item.
        /// This is displayed in the middle of the page, and the sub-text is not visible when it is displayed.
        /// </summary>
        public FileImageSource Icon
        {
            get => (FileImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets or sets the translucent foreground color combining with a Icon.
        /// Thereby producing a new blended color.
        /// [output alpha] = [source alpha] + [desitnation alpha] ( 1 - source alpha )
        /// [output RGB] = [source RGB] + [destination RGB] ( 1 - source alpha )
        /// Icon displays original colors color under default blending color RGBA(255, 255, 255, 255).
        /// </summary>
        public Color IconAlphaBlendingColor
        {
            get => (Color)GetValue(IconAlphaBlendingColorProperty);
            set => SetValue(IconAlphaBlendingColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the background image of item.
        /// This shows the background of an icon on the arc.
        /// </summary>
        public FileImageSource ItemBackgroundImage
        {
            get => (FileImageSource)GetValue(ItemBackgroundImageProperty);
            set => SetValue(ItemBackgroundImageProperty, value);
        }

        /// <summary>
        /// Gets or sets the icon image of item.
        /// /// This shows an icon on the arc.
        /// </summary>
        public FileImageSource ItemIcon
        {
            get => (FileImageSource)GetValue(ItemIconProperty);
            set => SetValue(ItemIconProperty, value);
        }

    }
}
