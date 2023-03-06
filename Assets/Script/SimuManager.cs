using UnityEngine;
using UnityEngine.UI;

public class SimuManager : MonoBehaviour
{
   public GameObject[] goPlanets;
   private float fZoomAmount, fUpAmount;

   // Start is called before the first frame update
   void Start()
   {
      Slider sldZoomManager = GameObject.Find("sldZoom").GetComponent<Slider>();
      sldZoomManager.maxValue = 100f;
      sldZoomManager.minValue = 0f;

      Slider sldUpManager = GameObject.Find("sldUp").GetComponent<Slider>();
      sldUpManager.maxValue = 100f;
      sldUpManager.minValue = 0f;

      foreach (GameObject go_i in goPlanets)
      {
         if(go_i.name != "Sun")
         {
            go_i.GetComponent<TrailRenderer>().startWidth = 10f;
            go_i.GetComponent<TrailRenderer>().endWidth = 10f;
         }

         string sName = go_i.name;
         float fDistanceOfCenter, fG, fMass, fSize;

         float fEarthMass = 5.972f * Mathf.Pow(10, 24);

         switch (sName)
         {
            case "Earth":
               fDistanceOfCenter = 151.48f;
               fG = 9.807f;
               fMass = 1.0f;
               fSize = 6.371f;
               break;
            case "Jupiter":
               fDistanceOfCenter = 755.91f;
               fG = 24.79f;
               fMass = 317.8f;// 1.898f * Mathf.Pow(10, 27) / fEarthMass;
               fSize = 69.911f;
               break;
            case "Mars":
               fDistanceOfCenter = 248.84f;
               fG = 3.721f;
               fMass = 0.107f; // 6.39f * Mathf.Pow(10, 23) / fEarthMass;
               fSize = 3.3895f;
               break;
            case "Mercury":
               fDistanceOfCenter = 63.81f;
               fG = 3.7f;
               fMass = 0.055f; //3.285f * Mathf.Pow(10, 23) / fEarthMass;
               fSize = 2.4397f;
               break;
            case "Neptune":
               fDistanceOfCenter = 4475.5f;
               fG = 11.15f;
               fMass = 17.150f; // 1.024f * Mathf.Pow(10, 26) / fEarthMass;
               fSize = 24.622f;
               transform.parent = go_i.transform;
               break;
            case "Saturn":
               fDistanceOfCenter = 1487.8f;
               fG = 10.44f;
               fMass = 95.160f; //5.683f * Mathf.Pow(10, 26) / fEarthMass;
               fSize = 58.232f;
               break;
            case "Sun":
               fDistanceOfCenter = 0f;
               fG = 274f;
               fMass = 333000f; // 1.989f * Mathf.Pow(10, 30) / fEarthMass;
               fSize = 696.340f;
               break;
            case "Uranus":
               fDistanceOfCenter = 2954.6f;
               fG = 8.87f;
               fMass = 14540f; // 8.681f * Mathf.Pow(10, 25) / fEarthMass;
               fSize = 25.362f;
               break;
            default:
               fDistanceOfCenter = 107.59f;
               fG = 8.87f;
               fMass = 0.815f; // 4.867f * Mathf.Pow(10, 24) / fEarthMass;
               fSize = 6.0518f;
               break;
         }
         fDistanceOfCenter *= 10f;
         fG = 100f;
         go_i.GetComponent<Planet>().InitPlanet(sName, fDistanceOfCenter, fG, fMass, fSize);  
      }
      InitVelocity();
   }

   private void InitVelocity()
   {
      foreach (GameObject go_i in goPlanets)
      {
         foreach (GameObject go_j in goPlanets)
         {
            if (!go_i.Equals(go_j))
            {
               Rigidbody rbPlanet_i = go_i.GetComponent<Rigidbody>();
               Rigidbody rbPlanet_j = go_j.GetComponent<Rigidbody>();

               if (rbPlanet_i && rbPlanet_j)
               {
                  float fDistance = Vector3.Distance(go_i.transform.position, go_j.transform.position);
                  go_i.transform.LookAt(go_j.transform);
                  float fG = go_i.GetComponent<Planet>().GetG();
                  rbPlanet_i.velocity += go_i.transform.right * Mathf.Sqrt((fG * rbPlanet_j.mass) * ((2 / fDistance) - (1 / (fDistance * 1.5f))));
               }
            }
         }
      }
   }

   private void ApplyGravity()
   {
      foreach (GameObject go_i in goPlanets)
      {
         foreach (GameObject go_j in goPlanets)
         {
            if (!go_i.Equals(go_j))
            {
               Rigidbody rbPlanet_i = go_i.GetComponent<Rigidbody>();
               Rigidbody rbPlanet_j = go_j.GetComponent<Rigidbody>();

               if (rbPlanet_i && rbPlanet_j)
               {
                  float fDistance = Vector3.Distance(go_i.transform.position, go_j.transform.position);
                  rbPlanet_i.AddForce((go_j.transform.position - go_i.transform.position).normalized * (go_i.GetComponent<Planet>().GetG() * (rbPlanet_i.mass * rbPlanet_j.mass) / Mathf.Pow(fDistance, 2)));
               }
            }
         }
      }
   }

   void FixedUpdate()
   {
      ApplyGravity();
   }

   void Update()
   {
      Vector3 v3PosCamera = transform.position;
      Vector3 v3PosNeptune = GameObject.Find("Neptune").transform.position;

      v3PosCamera.x = v3PosNeptune.x+20 - fZoomAmount / 100 * v3PosNeptune.x;
      v3PosCamera.z = v3PosNeptune.z - fZoomAmount / 100 * v3PosNeptune.z;
      v3PosCamera.y = v3PosNeptune.y+fUpAmount;

      transform.position = v3PosCamera;
   }

   public void SetZoomAmount(float fValue)
   {
      fZoomAmount = fValue;
   }

   public void SetUpAmount(float fValue)
   {
      fUpAmount = fValue;
   }
}
