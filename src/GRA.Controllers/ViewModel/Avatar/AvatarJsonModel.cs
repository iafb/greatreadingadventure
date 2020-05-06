using System;
using System.Collections.Generic;
using System.Text;

namespace GRA.Controllers.ViewModel.Avatar
{
    public class AvatarJsonModel
    {
        public ICollection<AvatarLayer> Layers { get; set; }

        public class AvatarLayer
        {
            public int Id { get; set; }

            public ICollection<AvatarItem> Items { get; set; }
            public ICollection<AvatarColor> Colors { get; set; }
        }

        public class AvatarItem
        {
            public int Id { get; set; }
            public string DateCreated { get; set; }
        }

        public class AvatarColor
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
    }
}
