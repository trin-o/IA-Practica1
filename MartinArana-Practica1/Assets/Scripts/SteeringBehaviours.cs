using UnityEngine;

namespace AI
{
    public class BaseAgent : MonoBehaviour
    {
        [HideInInspector]
        public Vector2 velocity, desiredVector, steer, randomTarget;
        [HideInInspector]
        public float randomTimer = 100;

        public float maxForce = 1, maxSteer = 1, visionRange = 4;

        public void addSeek(Vector2 target, float power=1)
        {
            desiredVector += SteeringBehaviours.SeekVector(this, target) * power;
        }
        public void addFlee(Vector2 target, float range, float power=1)
        {
            desiredVector += SteeringBehaviours.FleeVector(this, target, range) * power;
        }
        public void addRandom(Vector2 referencePosition, float changeTargetCD = .1f, float randomRadius = 4, float power=1)
        {
            desiredVector += SteeringBehaviours.RandomVector(this, referencePosition, changeTargetCD, randomRadius, power);
        }

        public void calculateMovement()
        {
            steer = Vector2.ClampMagnitude(desiredVector - velocity, maxSteer);
            desiredVector = Vector2.zero;

            velocity += steer;

            transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }

    static public class SteeringBehaviours
    {
        static public Vector2 SeekVector(BaseAgent agent, Vector2 targetPosition, float arriveRange = 1)
        {
            Vector2 desiredVector = targetPosition - (Vector2)agent.transform.position;
            desiredVector = Arrive(desiredVector, arriveRange);
            desiredVector *= agent.maxForce;
            return desiredVector;
        }

        static Vector2 Arrive(Vector2 desiredVector, float arriveRange)
        {
            if (desiredVector.magnitude > arriveRange)
                return desiredVector.normalized;
            else return desiredVector;
        }

        static public Vector2 FleeVector(BaseAgent agent, Vector2 targetPosition, float range)
        {
            Vector2 desiredVector;
            if (Vector2.Distance(targetPosition, agent.transform.position) < range)
            {
                desiredVector = (Vector2)agent.transform.position - targetPosition;
                return desiredVector.normalized;
            }
            else
            {
                return Vector2.zero;
            }
        }


        static public Vector2 RandomVector(BaseAgent agent, Vector2 referencePosition, float changeTargetCD, float randomRadius, float power)
        {

            agent.randomTimer += Time.deltaTime;
            if (agent.randomTimer > changeTargetCD)
            {
                agent.randomTarget = Random.insideUnitCircle * randomRadius + referencePosition;
                agent.randomTimer = 0;
            }

            return SeekVector(agent, agent.randomTarget,power);
        }
    }


}
