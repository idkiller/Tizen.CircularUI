using ElmSharp;
using System;
using System.Collections.Generic;
using System.IO;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(RotarySelectorPage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.RotarySelectorPageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    using global::System.Collections.Specialized;
    using global::System.ComponentModel;
    using ERotarySelectorItem = ElmSharp.Wearable.RotarySelectorItem;

    public class RotarySelectorPageRenderer : VisualElementRenderer<RotarySelectorPage>
    {
        ElmSharp.Wearable.RotarySelector _rotarySelector;
        readonly Dictionary<RotarySelectorItem, ERotarySelectorItem> _items = new Dictionary<RotarySelectorItem, ERotarySelectorItem>();

        readonly Dictionary<string, Action<ERotarySelectorItem, RotarySelectorItem>> _itemUpdateMap
            = new Dictionary<string, Action<ERotarySelectorItem, RotarySelectorItem>>
            {
                { RotarySelectorItem.TextProperty.PropertyName, UpdateText },
                { RotarySelectorItem.SubTextProperty.PropertyName, UpdateSubText },

                { RotarySelectorItem.IconProperty.PropertyName, UpdateIcon },
                { RotarySelectorItem.IconAlphaBlendingColorProperty.PropertyName, UpdateIconAlphaBlendingColor},

                { RotarySelectorItem.ItemBackgroundImageProperty.PropertyName, UpdateItemBackgroundImage },
                { RotarySelectorItem.ItemIconProperty.PropertyName, UpdateItemIcon }
            };

        public RotarySelectorPageRenderer()
        {
            RegisterPropertyHandler(RotarySelectorPage.BackgroundImageProperty, UpdateBackgroundImage);
            // TODO: native selected_set function is not working. implement it after bug patched.
            //RegisterPropertyHandler(RotarySelectorPage.SelectedItemProperty, UpdateSelectedItem);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RotarySelectorPage> e)
        {
            if (_rotarySelector == null)
            {
                _rotarySelector = new ElmSharp.Wearable.RotarySelector(Xamarin.Forms.Platform.Tizen.Forms.NativeParent)
                {
                    AlignmentX = NamedHint.Fill,
                    AlignmentY = NamedHint.Fill,
                    WeightX = NamedHint.Expand,
                    WeightY = NamedHint.Expand
                };

                _rotarySelector.Clicked += OnItemClicked;

                // TODO: native selected_set function is not working. implement it after bug patched.
                //_rotarySelector.Selected += OnItemSelected;

                SetNativeView(_rotarySelector);

                foreach (RotarySelectorItem item in Element.Items)
                {
                    UpdateItem(item);
                }
            }

            if (e.NewElement != null)
            {
                e.NewElement.Items.CollectionChanged += OnItemChanged;
            }
            if (e.OldElement != null)
            {
                e.OldElement.Items.CollectionChanged -= OnItemChanged;
            }

            base.OnElementChanged(e);
        }

        protected override void UpdateBackgroundColor(bool initialize)
        {
            if (_rotarySelector == null) return;
            if (initialize && Element.BackgroundColor.Equals(Color.Default)) return;
            var color = Element.BackgroundColor.ToNative();
            Interop.Eext.eext_rotary_selector_part_color_set(_rotarySelector,
                Interop.Eext.RotarySelectorBackgroundImagePartName,
                (int)Interop.Eext.RotarySelectorState.Normal,
                color.R, color.G, color.B, color.A);
        }

        protected void UpdateBackgroundImage(bool initialize)
        {
            if (_rotarySelector == null) return;
            if (initialize && Element.BackgroundImage == null) return;
            UpdateImage(img => _rotarySelector.BackgroundImage = img, _rotarySelector.BackgroundImage, Element.BackgroundImage);
        }


        void OnItemClicked(object sender, ElmSharp.Wearable.RotarySelectorItemEventArgs e)
        {
            foreach (KeyValuePair<RotarySelectorItem, ERotarySelectorItem> pair in _items)
            {
                if (pair.Value == e.Item)
                {
                    /* TODO: native selected_set function is not working. implement it after bug patched.
                    // even though item is actually selected, selected event has not been appeared.
                    // so I workaround it with this code.
                    if (Element.SelectedItem != pair.Key)
                    {
                        Element.SelectedItem = pair.Key;
                    }
                    */
                    pair.Key.Command?.Execute(pair.Key.CommandParameter);
                    break;
                }
            }
        }

        /* TODO: native selected_set function is not working. implement it after bug patched.
        void OnItemSelected(object sender, ElmSharp.Wearable.RotarySelectorItemEventArgs e)
        {
            if (e.Item == null) return;

            foreach (KeyValuePair<RotarySelectorItem, ERotarySelectorItem> pair in _items)
            {
                if (pair.Value == e.Item)
                {
                    Element.SelectedItem = pair.Key;
                    break;
                }
            }
        }

        void UpdateSelectedItem(bool initialize)
        {
            if (Element.SelectedItem == null) return;

            if (_items.TryGetValue(Element.SelectedItem, out var eItem))
            {
                global::System.Diagnostics.Debug.WriteLine($"selected item = {eItem.MainText}");
                _rotarySelector.SelectedItem = eItem;
            }
        }
        */

                    void UpdateItem(RotarySelectorItem item)
        {
            if (!_items.TryGetValue(item, out var eItem))
            {
                eItem = new ERotarySelectorItem();
                _rotarySelector.Items.Add(eItem);
                _items[item] = eItem;
                item.PropertyChanged += OnItemPropertyChanged;
            }

            foreach (var(key, func) in _itemUpdateMap)
            {
                func(eItem, item);
            }
        }

        void OnItemChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (RotarySelectorItem item in e.NewItems)
                {
                    UpdateItem(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (RotarySelectorItem item in e.OldItems)
                {
                    if (_items.TryGetValue(item, out var eItem))
                    {
                        _rotarySelector.Items.Remove(eItem);
                        _items.Remove(item);
                    }
                    item.PropertyChanged -= OnItemPropertyChanged;
                }
            }
        }

        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as RotarySelectorItem;
            if (_items.TryGetValue(item, out var eItem))
            {
                if (_itemUpdateMap.TryGetValue(e.PropertyName, out var updateFunc))
                {
                    updateFunc(eItem, item);
                }
            }
        }

        static void UpdateIcon(ERotarySelectorItem eItem, RotarySelectorItem item)
        {
            UpdateImage(img => eItem.SelectorIconImage = img, eItem.SelectorIconImage, item.Icon);
        }

        static void UpdateIconAlphaBlendingColor(ERotarySelectorItem eItem, RotarySelectorItem item)
        {
            if (eItem.Handle == null) return;

            var color = item.IconAlphaBlendingColor.ToNative();
            Interop.Eext.eext_rotary_selector_item_part_color_set(eItem.Handle,
                Interop.Eext.RotarySelectorIconPartName,
                (int)Interop.Eext.RotarySelectorState.Normal,
                color.R, color.G, color.B, color.A);
        }

        static void UpdateItemBackgroundImage(ERotarySelectorItem eItem, RotarySelectorItem item)
        {
            UpdateImage(img => eItem.NormalBackgroundImage = img, eItem.NormalBackgroundImage, item.ItemBackgroundImage);
        }

        static void UpdateItemIcon(ERotarySelectorItem eItem, RotarySelectorItem item)
        {
            UpdateImage(img => eItem.NormalIconImage = img, eItem.NormalIconImage, item.ItemIcon);
        }

        static void UpdateText(ERotarySelectorItem eItem, RotarySelectorItem item)
        {
            eItem.MainText = item.Text;
        }

        static void UpdateSubText(ERotarySelectorItem eItem, RotarySelectorItem item)
        {
            eItem.SubText = item.SubText;
        }

        static void UpdateImage(Action<ElmSharp.Image> updater, ElmSharp.Image img, Xamarin.Forms.FileImageSource value)
        {
            if (value == null)
            {
                updater(null);
                return;
            }
            string valueFile = Path.GetFullPath(ResourcePath.GetPath(value.File));
            if (img == null)
            {
                img = new ElmSharp.Image(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);
                img.Show();
                img.LoadAsync(valueFile);
                updater(img);
            }
            else
            {
                string propertyFile = Path.GetFullPath(img.File);
                if (!string.Equals(valueFile, propertyFile, StringComparison.Ordinal))
                {
                    img.LoadAsync(valueFile);
                }
            }
        }
    }
}
