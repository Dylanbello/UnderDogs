using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UINavSound : MonoBehaviour, ISelectHandler
{
    // To change the sound, use the audiosource located on the parent.
    [SerializeField] UIClipSound ui_ClipSound;
    public void OnSelect(BaseEventData eventData)
    {
        //if(eventData.selectedObject == gameObject) { SoundManager.Play2DSound(SoundManager.Sound.UISelection); }
        if(eventData.selectedObject == gameObject) { ui_ClipSound.PlayClip(ui_ClipSound.UISelected); }
    }

    private void Start() { GetComponent<Button>().onClick.AddListener(delegate { OnButtonPressed(); }); }
    private void OnButtonPressed() { ui_ClipSound.PlayClip(ui_ClipSound.UIOnClicked); }
}
