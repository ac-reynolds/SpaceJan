using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SlimeController : MonoBehaviour
{
    public int Range = 2;
    public float Speed = 1;
    public GameObject Player;

    private CharacterController _controller;
    private Vector2 _target;
    private bool _isHopping = false;
    private readonly float ZERO_EPSILON = .0001f;

    private bool _isThinking = false;
    private float _endThinkingTime;
    private readonly float THINK_TIME = 1.5f;

    private void Start() {
        _controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if ((_target - (Vector2)transform.position).sqrMagnitude < ZERO_EPSILON) {
            _isHopping = false;
        }

        if (!_isThinking && !_isHopping) {
            Think();
        }

        if (!_isHopping && Time.time > _endThinkingTime) {
            _isThinking = false;
            Hop(CalculateHopDirection(Player.transform.position));
        }
    }

    private void FixedUpdate() {
        if (_isHopping) {
            _controller.Move((_target - (Vector2)transform.position).normalized * Speed * Time.deltaTime);
        }
    }

    Vector2 CalculateHopDirection(Vector2 targetPos) {
        Vector2 displacement = targetPos - (Vector2)transform.position;
        Vector2 hopDirection = Vector2.zero;
        
        if (Mathf.Abs(displacement.x) < 1) {
            hopDirection.x = 0;
            hopDirection.y = 2 * Mathf.Sign(displacement.y);
        } else if (Mathf.Abs(displacement.y) < 1) {
            hopDirection.y = 0;
            hopDirection.x = 2 * Mathf.Sign(displacement.x);
        } else {
            hopDirection.x = Mathf.Sign(displacement.x);
            hopDirection.y = Mathf.Sign(displacement.y);
        }

        return hopDirection;
    }

    void Think() {
        Debug.Log("Thinking...");
        _endThinkingTime = Time.time + THINK_TIME;
        _isThinking = true;
    }

    /*
     * Hops in the target direction.  
     * 
     * If we're moving two units along an axis (e.g. [2,0]), hop will take
     * 2 * Speed seconds.  If we're moving diagonally (e.g. [1,1]), hop will
     * take sqrt(2) * Speed seconds (about 1.4142 * Speed seconds).
     */
    void Hop(Vector2 direction) {//[-2,0] [1,1]
        Debug.Log("Hopping towards " + direction + ". ");
        _target = (Vector2)transform.position + direction;
        _isHopping = true;
    }
}
