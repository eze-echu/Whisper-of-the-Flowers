using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace PopUps
{
    internal class PopUpBuilder
    {
        internal TMP_Text PopUpText;
        internal List<PopUp> parents;
        internal static PopUpBuilder NewBuilder()
        {
            PopUpBuilder builder = new PopUpBuilder();
            if (builder.PopUpText != null)
            {
                builder.PopUpText.text = "";
            }
            return builder;
        }

        public PopUpBuilder Text(TMP_Text text)
        {
            PopUpText = text;
            return this;
        }

        public PopUpBuilder Parents(List<PopUp> parentsParam)
        {
            this.parents = parentsParam;
            return this;
        }

        public PopUp Build()
        {
            var pop = new PopUp(this);
            return pop;
        }
    }
}