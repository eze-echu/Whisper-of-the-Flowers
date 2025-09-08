using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace PopUps
{
    [Serializable]
    public class PopUp: MonoBehaviour
    {
        // This variable is either an instance of this popup generated on creation
        // Or is a reference to self inside the instanced object
        public TMP_Text popUpText;
        private List<PopUp> _parentPopUps;
        private bool _popUpActive;
        private PopUp _childPopUp;

        internal PopUp(PopUpBuilder builder)
        {
            popUpText = builder.PopUpText;
            _parentPopUps = builder.parents;
        }

        internal PopUpBuilder Builder()
        {
            return PopUpBuilder.NewBuilder();
        }

        public void SetActive(bool active)
        {
            _popUpActive = active;
            foreach (var button in GetComponents<Button>())
            {
                button.interactable = active;
            }

            foreach (var slider in GetComponents<Slider>())
            {
                slider.SetEnabled(active);
            }
        }

        public bool IsActive()
        {
            return _popUpActive;
        }

        public void PopChild(GameObject child)
        {
            var parents = new List<PopUp>();

            if (parents.Count > 0)
            {
                parents.AddRange(_parentPopUps);
            }
            parents.Add(this);
            _childPopUp = Instantiate(child).GetComponent<PopUp>();
            _childPopUp._parentPopUps = parents;
            _childPopUp.OpenUp();
            _popUpActive = false;
        }

        public void ForceClose()
        {
            if (!_popUpActive)
            {
                _childPopUp.ForceClose();
            }
            CloseUp();
        }

        public PopUp OpenUp()
        {
            _popUpActive = true;
            SetActive(true);
            return this;
        }

        public void CopyFrom(PopUp other)
        {
            _popUpActive =  other.IsActive();
            _parentPopUps = other.Parents();
        }

        internal List<PopUp> Parents()
        {
            return _parentPopUps;
        }

        public void CloseUp()
        {
            if (!_popUpActive)
            {
                Debug.LogError("This PopUp doesn't appear to be active");
                return;
            }
            if (_parentPopUps is { Count: > 0 }) // This way, avoids crashing if var is null
            {
                _parentPopUps.Last().SetActive(true);
                _popUpActive = false;
                SetActive(false);
            }

            Destroy(gameObject);
        }
    }


}