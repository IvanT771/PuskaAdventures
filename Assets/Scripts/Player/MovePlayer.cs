using UnityEngine;


public class MovePlayer : MonoBehaviour
{
   [Header ("Камера игрока")]
   public Transform cameraPlayer;

   [Header("Тело игрока")]
   public Transform bodyPlayer;
    //  private CharacterController controller = null;
    private Rigidbody rb = null;
    [Header("Задержка повторного прыжка")]
    public float timePause = 3f;
    [Header("Скорость ходьбы")]
    public float speedPlayer = 2f;
    [Header("Сила прыжка")]
    public float jumpForce = 1000f;
    private Animator animator = null;

    Vector3 _up; //лежит ли игрок

    private bool stopInGo = true;
    private float timeDelay = 1f;



   
    void Start()
    {
             _up = bodyPlayer.transform.up;
            animator = bodyPlayer.GetComponent<Animator>();
        //  controller = GetComponent<CharacterController>();
            rb = bodyPlayer.GetComponent<Rigidbody>();
         
    }
    private void Update()
    {
        ProverkaNaRotation();
        Jump();
        if (stopInGo) {
            KeyControl();
            Move_Body();
            GetSpeedPlayer(); }
       // Move_Character();
    }
    private void Jump()
    {
        timeDelay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeDelay >= timePause)
        {
            rb.angularVelocity = Vector3.zero; //если убрать, то будет очень весело
            bodyPlayer.localEulerAngles = new Vector3(0, bodyPlayer.localEulerAngles.y, 0);
            rb.AddForce(_up * jumpForce);
            timeDelay = 0;
        }
    }
    private void FixedUpdate()
    {
        if (stopInGo)
        {
            Move_Character();
        }
    }

    //Управление с клавиатуры и включение анимации
    private void KeyControl() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) animator.SetBool("Walk", true); else { animator.SetBool("Walk", false); }
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) { animator.SetBool("LeftWalk", true); } else { animator.SetBool("LeftWalk", false); }
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) { animator.SetBool("RightWalk", true); } else { animator.SetBool("RightWalk", false); }

    }
   
    void ProverkaNaRotation()
    {
        if (Vector3.SignedAngle(_up, bodyPlayer.transform.up, bodyPlayer.transform.right) > 80 || Vector3.SignedAngle(_up, bodyPlayer.transform.up, bodyPlayer.transform.right) < -80)
        {
            stopInGo = false;
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
            stopInGo = true;
        }
    }
   
    //Вращение тела игрока
    private void Move_Body(){
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            bodyPlayer.eulerAngles = new Vector3(bodyPlayer.eulerAngles.x,cameraPlayer.eulerAngles.y,bodyPlayer.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            bodyPlayer.eulerAngles = new Vector3(bodyPlayer.eulerAngles.x,cameraPlayer.eulerAngles.y,bodyPlayer.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            bodyPlayer.eulerAngles = new Vector3(bodyPlayer.eulerAngles.x, cameraPlayer.eulerAngles.y-30, bodyPlayer.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            bodyPlayer.eulerAngles = new Vector3(bodyPlayer.eulerAngles.x, cameraPlayer.eulerAngles.y + 30, bodyPlayer.eulerAngles.z);
        }
    }



    //Движение игрока за счет character controler

    Vector3 velocity = Vector3.zero;

    void GetSpeedPlayer()
    {
        var Move_horizontal = Input.GetAxisRaw("Horizontal");
        var Move_vertical = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) { Move_horizontal = 0; }
        velocity = (bodyPlayer.forward * Move_vertical + bodyPlayer.right * Move_horizontal).normalized;
    }

    void Move_Character()
    {
        //var Move_horizontal = Input.GetAxisRaw("Horizontal");
        //var Move_vertical = Input.GetAxisRaw("Vertical");

        
        //if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) 
        //    || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)
        //    || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)
        //    || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) { Move_horizontal = 0; }
        //velocity = (bodyPlayer.forward * Move_vertical + bodyPlayer.right * Move_horizontal).normalized;

        //if ((Input.GetButton("Jump") && controller.isGrounded)  )
        //{
        //    velocity_Y = jumpForce;


        //}

        //if(!controller.isGrounded){
        //velocity_Y-=6.8f*Time.deltaTime;}else{
        //    if(velocity_Y != jumpForce){
        //        velocity_Y = 0;
        //    }
        //}

        //velocity.y = velocity_Y;

        //controller.Move(velocity* speedPlayer*Time.deltaTime);
        rb.MovePosition(rb.position+velocity * speedPlayer * Time.deltaTime);
    }

   
}
