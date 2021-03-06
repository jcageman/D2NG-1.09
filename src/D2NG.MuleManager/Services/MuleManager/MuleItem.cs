﻿using D2NG.Core.D2GS.Items;
using System.Collections.Generic;

namespace D2NG.MuleManager.Services.MuleManager
{
    public class MuleItem
    {
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string CharacterName { get; set; }
        public string ItemName { get; set; }
        public string QualityType { get; set; }
        public string ClassificationType { get; set; }

        public Dictionary<string, int> Stats { get; set; }
    }
}
