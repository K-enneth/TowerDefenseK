using Managers;
using Towers;
using UnityEngine;
public class DragDrop : MonoBehaviour
{
    #region Variables
        private Vector3 _offset;
        private GameObject _draggingObject;
        private Tower _tower;
        private bool _canDrag = true; 
        public Canvas infoCanvas;
    #endregion
    
    private void OnEnable()
    {
        CoinManager.CoinCountChanged += CanbeDragged;
        _tower = gameObject.GetComponent<Tower>();

    }
    
    private void OnDisable()
    {
        CoinManager.CoinCountChanged -= CanbeDragged;
    }
    
    private void OnMouseEnter()
    {
        infoCanvas.enabled = true;
    }

    private void OnMouseExit()
    {
        infoCanvas.enabled = false;
    }
    
    private void OnMouseDown()
    {
        if(!_canDrag)return;
        _draggingObject = Instantiate(gameObject,transform.position,Quaternion.identity);
        infoCanvas.enabled = false;
        _draggingObject.transform.GetChild(0).gameObject.SetActive(true);
        _offset = transform.position - MousePos();
        _draggingObject.transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnMouseDrag()
    {
        if(!_canDrag)return;
        _draggingObject.transform.position = MousePos() + _offset;
    }

    private void OnMouseUp()
    {
        if(!_canDrag)return;
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MousePos() - Camera.main.transform.position;
        _draggingObject.transform.GetChild(0).gameObject.SetActive(false);
        
        //Checking if it´s a valid tile to place bomb in
        if (Physics.Raycast(rayOrigin, rayDirection, out var hitInfo) &&
            hitInfo.collider.gameObject.TryGetComponent(out Tile tile) &&
            !tile.hasTower)
        {
            //Disabling drag and drop elements and enabling attack ones
            tile.hasTower = true;
            CoinManager.instance.WasteCoin(_tower.cost);
            _draggingObject.GetComponent<Tower>().enabled = true;
            _draggingObject.GetComponent<SphereCollider>().enabled = true;
            _draggingObject.transform.position = hitInfo.transform.position;
            _draggingObject.layer = 2;
            
            _draggingObject.GetComponent<DragDrop>()._canDrag = false;
            _draggingObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            Destroy(_draggingObject);
        }
        
    }

    private void CanbeDragged(int coins)
    {
        //Updating if player has enough money to use bomb
        _canDrag = _tower.cost <= coins ? true : false;
    }

    
    
    private Vector3 MousePos()
    {
        var screenPos = Input.mousePosition;
        screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}