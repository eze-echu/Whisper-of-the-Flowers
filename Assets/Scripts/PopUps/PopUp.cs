using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace PopUps
{
    [Serializable]
    public class PopUp: MonoBehaviour
    {
        [SerializeField]
        public GameObject popUpBox;
        // This variable is either an instance of this popup generated on creation
        // Or is a reference to self inside the instanced object
        private GameObject _popUpBoxInstance;
        public TMP_Text popUpText;
        private List<PopUp> _parentPopUps;
        private bool _popUpActive;
        private PopUp _childPopUp;

        internal PopUp(PopUpBuilder builder)
        {
            popUpBox = builder.popUpBox;
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
        }

        public bool IsActive()
        {
            return _popUpActive;
        }

        public void PopChild(PopUp child)
        {
            var parents = new List<PopUp>();
            if (child.popUpText == null)
            {
                child.popUpText = popUpBox.AddComponent<TextMeshPro>();
            }

            if (parents.Count > 0)
            {
                parents.AddRange(_parentPopUps);
            }
            parents.Add(this);
            _childPopUp = Builder()
                .Parents(parents)
                .Text(child.popUpText)
                .PopUpBox(child.popUpBox)
                .Build().OpenUp();
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
            _popUpBoxInstance = Instantiate(popUpBox);
            _popUpActive = true;
            _popUpBoxInstance.SetActive(true);
            _popUpBoxInstance.GetComponent<PopUp>().CopyFrom(this);
            _popUpBoxInstance.GetComponent<PopUp>().SetInstanceToSelf();
            return this;
        }

        public void CopyFrom(PopUp other)
        {
            _popUpActive =  other.IsActive();
            _parentPopUps = other.Parents();
        }

        internal void SetInstanceToSelf()
        {
            _popUpBoxInstance = gameObject;
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
            if (_parentPopUps.Count > 0)
            {
                _parentPopUps.Last().SetActive(true);
                _popUpActive = false;
                _popUpBoxInstance.SetActive(false);
            }
            else
            {
                _popUpBoxInstance.SetActive(false);
                GameState.ResumeGame();
            }

            Destroy(_popUpBoxInstance);
        }
    }


}