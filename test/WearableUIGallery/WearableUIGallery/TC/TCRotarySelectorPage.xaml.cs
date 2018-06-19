using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCRotarySelectorPage : RotarySelectorPage
    {
        int index = 1;
        bool increment = true;

        Command _cmd;

        Color[] _blends = { Color.White, Color.Red, Color.Blue, Color.Pink, Color.Peru, Color.Green };

        public TCRotarySelectorPage ()
		{
            _cmd = new Command(DoCommand);
            InitializeComponent ();
		}

        public ICommand ClickCommand => _cmd;

        void DoCommand()
        {
            if (index > 14)
            {
                increment = false;
            }
            else if (index < 1)
            {
                increment = true;
            }

            if (increment)
            {
                index++;
                var id = index % 10 + 1;
                var item = new RotarySelectorItem
                {
                    Text = $"Text_{id}",
                    SubText = $"SubText_{id}",
                    ItemIcon = new FileImageSource { File = $"image/icon56x56_{id}.png" },
                    Command = new Command(DoCommand)
                };
                if (id % 2 == 0)
                {
                    item.Icon = new FileImageSource { File = $"image/icon56x56_{id}.png" };
                    item.IconAlphaBlendingColor = _blends[new Random().Next(_blends.Length)];
                }
                Items.Add(item);
            }
            else
            {
                index--;
                Items.RemoveAt(Items.Count-1);
            }
        }
	}
}