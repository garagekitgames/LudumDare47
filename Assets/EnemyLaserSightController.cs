using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyLaserSightController : MonoBehaviour
{
    public LayerMask wallCollisionMask;
    public float drawPathLength = 10f;
    public LineRenderer lineRenderer;
    public bool laserOn = false;
    public bool reflect = false;

    public Gradient fireGradient;
    public Material fireMaterial;
    public Gradient chargeGradient;
    public Material aimMaterial;
    public Gradient aimGradient;

    public Color chargeColor;
    public Color fireColor;
    public Color aimColor;
    public Ease fireWidthEase;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        laserOn = true;
        lineRenderer.colorGradient = aimGradient;
        lineRenderer.widthMultiplier = 1f;
        lineRenderer.material = aimMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(laserOn)
        {
            DrawPath(transform.position, transform.forward);
        }
    }

    public void DrawPath(Vector3 position, Vector3 direction)
    {
        position.y = 1.5f;
        lineRenderer.SetPosition(0, position);
        //lineRenderer.SetPosition(1, position + currentHeading * 5);

        Vector3 direction2 = Vector3.zero;

        // DrawPath(position, direction, reflectionsRemaining - 1, index + 1);

        //Ray ray2 = new Ray(transform.position, previousHeading);
        //RaycastHit hitInfo2;


        //if (Physics.SphereCast(position, 0.2f, direction, out hit, 10)) //(Physics.Raycast(ray2, out hitInfo2, 5f, collisionMask))
        //{

        //}

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if(reflect)
        {
            lineRenderer.positionCount = 3;
            if (Physics.SphereCast(position, 0.35f, direction, out hit, drawPathLength, wallCollisionMask)) //(Physics.Raycast(ray, out hit, 10f))
            {
                direction2 = Vector3.Reflect(direction, hit.normal);
                var hitPoint = hit.point;
                hitPoint.y = 1.5f;
                lineRenderer.SetPosition(1, hitPoint);
                var distance = drawPathLength - (hitPoint - position).magnitude;
                lineRenderer.SetPosition(2, hitPoint + direction2 * (distance + 2.5f));


                //var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
                //newRotation.x = 0;
                //newRotation.z = 0;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            }
            else
            {
                lineRenderer.SetPosition(1, position + direction * drawPathLength / 2);
                lineRenderer.SetPosition(2, position + direction * drawPathLength);

                //var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
                //newRotation.x = 0;
                //newRotation.z = 0;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            lineRenderer.positionCount = 2;
            if (Physics.SphereCast(position, 0.35f, direction, out hit, drawPathLength, wallCollisionMask)) //(Physics.Raycast(ray, out hit, 10f))
            {
                //direction2 = Vector3.Reflect(direction, hit.normal);
                var hitPoint = hit.point;
                hitPoint.y = 1.5f;
                lineRenderer.SetPosition(1, hitPoint);
                //var distance = drawPathLength - (hitPoint - position).magnitude;
                //lineRenderer.SetPosition(2, hitPoint + direction2 * (distance + 2.5f));


                //var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
                //newRotation.x = 0;
                //newRotation.z = 0;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            }
            else
            {
                lineRenderer.SetPosition(1, position + direction);
                //lineRenderer.SetPosition(2, position + direction * drawPathLength);

                //var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
                //newRotation.x = 0;
                //newRotation.z = 0;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            }
        }
        


    }

    public void StopLaser()
    {
        laserOn = false;
        lineRenderer.colorGradient = aimGradient;
        lineRenderer.widthMultiplier = 1f;
        lineRenderer.material = aimMaterial;
    }

    public void StartLaserCharge()
    {
        

        laserOn = true;
        lineRenderer.colorGradient = chargeGradient;
        lineRenderer.widthMultiplier = 0.25f;
        lineRenderer.material = aimMaterial;
        //DOTween.To(() => lineRenderer.widthMultiplier, x => lineRenderer.widthMultiplier = x, 0.25f, 0.45f);

        
        DOTween.To(() => lineRenderer.startColor, co => { lineRenderer.startColor = co; }, chargeColor, 0.45f);
        DOTween.To(() => lineRenderer.endColor, co => { lineRenderer.endColor = co; }, chargeColor, 0.45f);
        
        //increase intensity ? of laser
        //Lerp from white to red by charge-time seconds, then  
    }

    public void StartLaserFire()
    {
        laserOn = true;
        lineRenderer.colorGradient = fireGradient;
        //lineRenderer.widthMultiplier = 1f;
        lineRenderer.material = fireMaterial;

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(DOTween.To(() => lineRenderer.widthMultiplier, x => lineRenderer.widthMultiplier = x, 0.5f, 0.05f).SetEase(fireWidthEase));
        mySequence.Append(DOTween.To(() => lineRenderer.widthMultiplier, x => lineRenderer.widthMultiplier = x, 0f, 0.05f).SetEase(fireWidthEase));

        //increase intensity ? of laser
        //Lerp from white to red by charge-time seconds, then  
    }

    public void StartLaserAim()
    {
        laserOn = true;
        lineRenderer.colorGradient = aimGradient;
        lineRenderer.widthMultiplier = 1f;
        lineRenderer.material = aimMaterial;
        //DOTween.To(() => lineRenderer.endWidth, x => lineRenderer.widthMultiplier = x, 0.5f, 0.1f);
        //
    }

    
}
