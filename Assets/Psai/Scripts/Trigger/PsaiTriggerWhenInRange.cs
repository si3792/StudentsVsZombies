﻿//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using psai.net;

public class PsaiTriggerWhenInRange : PsaiColliderBasedTrigger
{    
    public float triggerRadius = 40;
    public float minimumIntensity = 0.5f;
    public float maximumIntensity = 1.0f;
    public bool scaleIntensityByDistance = true;

    /// <summary>
    /// The distance between the player and this.gameObject. We store this so we only need to calculate it once.
    /// </summary>
    private float _lastDistanceCalculated;


    public PsaiTriggerWhenInRange()
    {
        this.triggerEvaluationNeedsUpdateMethod = true;
    }

    private void CalculateDistance()
    {     
        if (localCollider != null && playerCollider != null)
        {            
            //Vector3 closestPointOnThisCollider = localCollider.ClosestPointOnBounds(playerCollider.gameObject.transform.position);
            //_lastDistanceCalculated = (closestPointOnThisCollider - playerCollider.ClosestPointOnBounds(closestPointOnThisCollider)).magnitude;
        }
        else
        {
            //_lastDistanceCalculated = (gameObject.transform.position - playerCollider.ClosestPointOnBounds(gameObject.transform.position)).magnitude;
        }

		_lastDistanceCalculated = Vector2.Distance(localCollider.transform.position, playerCollider.transform.position);
    }

    public override bool EvaluateTriggerCondition()
    {
        CalculateDistance();
        return  (_lastDistanceCalculated < triggerRadius);
    }

    void OnCreate()
    {
		this.playerCollider = (Collider2D) GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
		this.localCollider = (Collider2D) this.gameObject.GetComponent<CircleCollider2D>();
    }

    public override float CalculateTriggerIntensity()
    {
        if (scaleIntensityByDistance)
        {
            float distanceRatio = 1.0f - (_lastDistanceCalculated / triggerRadius);
            float triggerIntensity = Mathf.Lerp(minimumIntensity, maximumIntensity, distanceRatio);

            //Debug.Log("distance:" + _lastDistanceCalculated + " radius:" + triggerRadius + " distranceRadio:" + distanceRatio + "  returning triggerIntensity " + triggerIntensity);
            return triggerIntensity;
        }
        else
        {
            return maximumIntensity;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, this.triggerRadius);
    }
}
