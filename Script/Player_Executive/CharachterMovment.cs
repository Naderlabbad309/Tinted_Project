using Tec.CameraFollow;
using UnityEngine;
using static Tec.TecVector;


public class CharachterMovment : MonoBehaviour, IPlayerable
{
    [SerializeField] private PlayerSystem playersystem;
    public PlayerSystem PlayerSystem => playersystem;

    void Start()
    {
        GameEventManager.Instance.OnHealthEvent += playersystem.Heal;
    }
    void Awake()
    {
        playersystem.GamePlayerData(GameDataBase.Instance.playerinfo);
    }
    void Update()
    {
        //KeyBoardInput
    }
    void FixedUpdate()
    {
        playersystem.JumbCheck();
    }
    void LateUpdate()
    {
        CameraFollowSystem.CameraFollow(gameObject.transform);
        KeyCodeInput();
    }
    private void KeyCodeInput()
    {
        if (playersystem.isdead) { return; }
        bool isLeftPressed = Input.GetKey(KeyCode.LeftArrow);
        bool isRightPressed = Input.GetKey(KeyCode.RightArrow);
        bool isWalking = isLeftPressed || isRightPressed;
        if (!isWalking) { playersystem.Anim.SetBool("Walk", false); }
        else
        {
            Vector3 moveDirection = isLeftPressed ? Vector3.left : Vector3.right;
            bool isFlipped = isLeftPressed; // Assuming left is flipped, right is normal
            if (playersystem.JumbCheck())
            { transform.Translate(moveDirection * Time.deltaTime * playersystem.Speed * 2.5f, Space.World); }
            else { transform.Translate(moveDirection * Time.deltaTime * playersystem.Speed, Space.World);}
            
            ((IPlayerable)this).FlipPlayer(isFlipped);
            playersystem.Anim.SetBool("Walk", true);

        }
        bool isUpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        if (isUpPressed && !playersystem.JumbCheck()) { playersystem.Jumb();}
        bool isQPressed = Input.GetKeyDown(KeyCode.Q);
        if (isQPressed) { playersystem.Slash(new DuoVector(Vector2.left, Vector2.right), GameDataBase.Instance.playerinfo.Damage_info);}
        bool isShootPressed = Input.GetKey(KeyCode.E);
        if (isShootPressed) { playersystem.Shoot();}

    }


    void IPlayerable.FlipPlayer(bool XFlip) {gameObject.GetComponent<SpriteRenderer>().flipX = XFlip; }
    void IPlayerable.DeathEvent() { playersystem.isdead = true;GameEventManager.Instance.MainPlayerDied(); }
    void IPlayerable.SlashStopEvent() {playersystem.Anim.SetBool("Slash", false); }
    void IPlayerable.ShootStopEvent() {playersystem.Anim.SetBool("Shoot", false); }

}
