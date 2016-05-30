using UnityEngine;
using System.Collections;
namespace Menus
{
    public class BaseMenu : MonoBehaviour
    {

        public event VoidDelegate onClose;

        /// <summary>
        /// Open menu
        /// 
        /// by default it just enables and disables the game object
        /// </summary>
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Closes menu
        /// 
        /// by default it sends a event when the menu is closed
        /// the menu is closed by disabling the game object
        /// </summary>
        public virtual void Close()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            gameObject.SetActive(false);

            if (onClose != null)
            {
                onClose();
                onClose = null;
            }

        }
    }
}