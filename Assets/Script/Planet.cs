using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Planet : MonoBehaviour
{
   private string sName;
   private float fDistanceOfCenter, fDistanceToMakeTurn, fG;
   private Rigidbody rigidBody;

   public void SetName(string sValue)
   {
      sName = sValue;
   }

   public string GetName()
   {
      return sName;
   }

   private float GetDistanceToMakeTurn()
   {
      return fDistanceToMakeTurn;
   }

   public void SetRigidBody(Rigidbody rbValue)
   {
      rigidBody = rbValue;
   }

   public Rigidbody GetRigidbody()
   {
      return rigidBody;
   }

   public void SetfDistanceOfCenter(float fValue)
   {
      fDistanceOfCenter = fValue;
      transform.position = new Vector3(fDistanceOfCenter, 0, 0);
   }

   public float GetfDistanceOfCenter()
   {
      return fDistanceOfCenter;
   }

   public void SetG(float fValue)
   {
      fG = fValue;
   }

   public float GetG()
   {
      return fG;
   }

   public void SetDistanceToMakeTurn()
   {
      fDistanceToMakeTurn = 2 * Mathf.PI * fDistanceOfCenter;
   }

   public void SetMass(float fValue)
   {
      rigidBody.mass = fValue;
   }

   public void SetSize(float fValue)
   {
      transform.localScale = new Vector3(fValue, fValue, fValue);
   }

   public void InitPlanet(string sName, float fDistancOfCenter, float fG, float fMass, float fSize) //fDistancOfCenter and fSize are in meters, fMass is in kg
   {
      SetName(sName);
      SetfDistanceOfCenter(fDistancOfCenter);
      SetG(fG);
      SetRigidBody(GetComponent<Rigidbody>());
      SetMass(fMass);
      SetSize(fSize);
      SetDistanceToMakeTurn();
   }
}
