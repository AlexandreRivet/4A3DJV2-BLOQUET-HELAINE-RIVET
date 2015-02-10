using UnityEngine;
using System.Collections;

public class OnContactObjectScript : MonoBehaviour {

    private bool _isContact = false;
    private bool _isTrigger = false;
    private GameObject _objectContacted;
    private GameObject _objectTrigger;

    //Gestion des contacts entre les objets du decors et les personnages, pas encore au points
    public void setIsContact(bool value)
    {
        _isContact = value;
    }
    public bool isContact()
    {
        return _isContact;
    }
    public void setIsTrigger(bool value)
    {
        _isTrigger = value;
    }
    public bool isTrigger()
    {
        return _isTrigger;
    }
    public void setObjectContacted(GameObject objectContacted)
    {
        _objectContacted = objectContacted;
    }
    public GameObject getObjectContacted()
    {
        return _objectContacted;
    }
    public void setObjectTrigger(GameObject objectTrigger)
    {
        _objectTrigger = objectTrigger;
    }
    public GameObject getObjectTrigger()
    {
        return _objectTrigger;
    }
    public bool objectCanStopMe()
    {
        if (_objectContacted != null)
            return _objectContacted.layer == 9;

        return false;
    }
    public bool objectIsTeleporter()
    {
        if (_objectContacted != null)
            return _objectContacted.layer == 10;

        return false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 9)
            return;

        _isContact = true;
        _objectContacted = collision.gameObject;
        //Debug.Log("Enter" + _objectContacted.name);
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != 9)
            return;
        //Debug.Log("Exit" + _objectContacted.name);
        _isContact = false;
        _objectContacted = null;
        
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != 10)
            return;
        _isTrigger = true;
        _objectTrigger = collider.gameObject;
        //Debug.Log("Enter" + _objectTrigger.name);
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer != 10)
            return;
        //Debug.Log("Exit" + _objectTrigger.name);
        _isTrigger = false;
        _objectTrigger = null;
       
    }
}
