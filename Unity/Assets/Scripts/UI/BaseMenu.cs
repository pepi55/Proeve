using UnityEngine;
using System.Collections;

public class BaseMenu : MonoBehaviour {

    public event VoidDelegate onClose;

	public virtual void Open()
    {
        gameObject.SetActive(true);
    }

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
