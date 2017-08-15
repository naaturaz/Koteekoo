using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIElement : General
{

    //inipos is used for Hide and show 
    protected Vector3 iniPos;

    protected InputField _titleInputField;
    protected GameObject _titleInputFieldGO;
    private Scrollbar _verticScrollbar;

    RectTransform _scrollContent;

    // Use this for initialization
    protected void Start()
    {

        _titleInputFieldGO = GetGrandChildCalled("TitleInputField");
        _titleInputField = _titleInputFieldGO.GetComponent<InputField>();
        _titleInputFieldGO.SetActive(false);

    }




    // Update is called once per frame
    protected void Update()
    {


    }

    public void Show()
    {
        transform.position = iniPos;
    }

    public bool IsShownNow()
    {
        return transform.position.y > 0;
        //return UMath.nearEqualByDistance(transform.position, iniPos, 0.3f);
    }

    protected void MakeAlphaColorZero(Text g)
    {
        Color bl = Color.white;
        bl.a = 0f;
        g.color = bl;
    }

    protected void MakeAlphaColorMax(Text g)
    {
        Color bl = Color.white;
        bl.a = 255f;
        g.color = bl;
    }

    protected Rect GetRectFromBoxCollider2D(Transform t)
    {
        var res = new Rect();
        var min = t.GetComponent<BoxCollider2D>().bounds.min;
        var max = t.GetComponent<BoxCollider2D>().bounds.max;

        res = new Rect();
        res.min = min;
        res.max = max;

        return res;
    }

    /// <summary>
    /// Hides the element 
    /// </summary>
    public virtual void Hide()
    {
        if (name.Contains("Bulletin"))
        {
            //Program.gameScene.TutoStepCompleted("HideBulletin.Tuto");
        }
        if (name.Contains("Building"))
        {
            //Program.gameScene.TutoStepCompleted("CloseDockWindow.Tuto");
        }

        Vector3 newPos = transform.position;
        newPos.y = -800f;
        transform.position = newPos;
    }



    

    internal void HideArrow()
    {
        var arrowGO = GetChildCalled("Arrow");

        if (arrowGO == null)
        {
            return;
        }

        arrowGO.SetActive(false);
    }











}
