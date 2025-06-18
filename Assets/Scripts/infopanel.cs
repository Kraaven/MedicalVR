using TMPro;
using UnityEngine;

public class infopanel : MonoBehaviour
{
    
    [SerializeField] TMP_Text _infoOrganName;
    [SerializeField] TMP_Text _infoOrganDescription;

    public void ShowInfo(OrganInfo organInfo)
    {
        _infoOrganName.text = organInfo.OrganName;
        _infoOrganDescription.text = organInfo.OrganDescription;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
