using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public Transform[] portals;  // Portal konumlar�
    private bool canTeleport = true;  // Teleport yapma izni
    private bool isInPortal = false;  // Portaldan ge�i� yap�lm�� m�?
    private int lastPortalIndex = -1;  // Son ge�ilen portal�n indexi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTeleport && !isInPortal)
        {
            for (int i = 0; i < portals.Length; i++)
            {
                // Portal�n tag'ini kar��la�t�rarak hangi portaldan ge�ildi�ini tespit et
                if (collision.gameObject.CompareTag((i).ToString()))
                {
                    // Mevcut portal�n indexine g�re sonraki portala git
                    if (i + 1 < portals.Length)
                    {
                        transform.position = portals[i + 1].position;  // Sonraki portala git
                    }
                    else
                    {
                        transform.position = portals[0].position;  // Son portaldan sonra, ba�a d�n
                    }

                    // Girilen portal� yok et
                    Destroy(collision.gameObject);

                    // Ge�ilen portallar� yok etmek i�in portallar� s�f�rla
                    // Sadece bir sonraki portal� aktif et, �nceki portal� engelle
                    lastPortalIndex = i;
                    DisablePreviousPortals(i); // Geriye d�n�lememesi i�in portallar� devre d��� b�rak

                    isInPortal = true;  // �u anda portaldan i�eri girmekteyiz
                    canTeleport = false;  // Teleport yapmay� engelle
                    StartCoroutine(ResetTeleport());  // Teleportu belirli bir s�re beklet
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
