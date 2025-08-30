using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace PopUps
{
    internal class PopUpBuilder
    {
        internal TMP_Text PopUpText;
        internal GameObject popUpBox;
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

        public PopUpBuilder PopUpBox(GameObject popUpBoxParam)
        {
            this.popUpBox = popUpBoxParam;
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