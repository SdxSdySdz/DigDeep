using CodeBase.GameLogic.Player;
using UnityEngine;

namespace CodeBase.GameLogic.Climbing
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private LadderEdge _topEdge;
        [SerializeField] private LadderEdge _bottomEdge;

        [Header("Floor points")] 
        [SerializeField] private Transform _topFloorPoint;
        [SerializeField] private Transform _bottomFloorPoint;
        
        private void OnEnable()
        {
            _topEdge.Reached += OnEdgeReached;
            _bottomEdge.Reached += OnEdgeReached;
        }

        private void OnDisable()
        {
            _topEdge.Reached -= OnEdgeReached;
            _bottomEdge.Reached -= OnEdgeReached;
        }

        private void OnEdgeReached(LadderEdge edge, Character character)
        {
            Vector3 climbingPosition = edge.transform.position - edge.transform.forward * 0.5f;
            if (edge == _topEdge)
            {
                if (character.IsClimbing)
                {
                    character.StopClimbing(_topFloorPoint.position);
                }
                else
                {
                    character.Climb(climbingPosition);
                }
            }
            else if (edge == _bottomEdge)
            {
                if (character.IsClimbing)
                {
                    character.StopClimbing(_bottomFloorPoint.position);
                }
                else
                {
                    character.Climb(climbingPosition);
                }
            }
        }
    }
}