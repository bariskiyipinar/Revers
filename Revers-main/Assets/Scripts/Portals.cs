using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public Transform[] portals;  // Portal konumlarý
    private bool canTeleport = true;  // Teleport yapma izni
    private bool isInPortal = false;  // Portaldan geçiþ yapýlmýþ mý?
    private int lastPortalIndex = -1;  // Son geçilen portalýn indexi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTeleport && !isInPortal)
        {
            for (int i = 0; i < portals.Length; i++)
            {
                // Portalýn tag'ini karþýlaþtýrarak hangi portaldan geçildiðini tespit et
                if (collision.gameObject.CompareTag((i).ToString()))
                {
                    // Mevcut portalýn indexine göre sonraki portala git
                    if (i + 1 < portals.Length)
                    {
                        transform.position = portals[i + 1].position;  // Sonraki portala git
                    }
                    else
                    {
                        transform.position = portals[0].position;  // Son portaldan sonra, baþa dön
                    }

                    // Girilen portalý yok et
                    Destroy(collision.gameObject);

                    // Geçilen portallarý yok etmek için portallarý sýfýrla
                    // Sadece bir sonraki portalý aktif et, önceki portalý engelle
                    lastPortalIndex = i;
                    DisablePreviousPortals(i); // Geriye dönülememesi için portallarý devre dýþý býrak

                    isInPortal = true;  // Þu anda portaldan içeri girmekteyiz
                    canTeleport = false;  // Teleport yapmayý engelle
                    StartCoroutine(ResetTeleport());  // Teleportu belirli bir süre beklet
                    break;
                }
            }
        }
    }


    private void DisablePreviousPortals(int currentPortalIndex)
    {
        for (int i = 0; i < currentPortalIndex; i++)
        {
            portals[i] = null; 
        }
    }

    private IEnumerator ResetTeleport()
    {
        yield return new WaitForSeconds(0.5f);  
        canTeleport = true;
        isInPortal = false; 
    }
}
