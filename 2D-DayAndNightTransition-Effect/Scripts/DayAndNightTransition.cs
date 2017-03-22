using UnityEngine;
using System.Collections;
public class DayAndNightTransition : MonoBehaviour {
	public Material mat;
	public Color originCol;
	public float upperLimit = 0.7f;

	void Start()
	{
		mat = this.GetComponent<BaseImageEffets>().mat;
		originCol = new Color(0.2f,0.2f,0.2f,1.0f);
		mat.SetColor("_NightColor",originCol);
		mat.SetInt("_boolDay",0);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
		{
			mat.SetInt("_boolDay",mat.GetInt("_boolDay")+1);
			//print(mat.GetInt("_boolDay"));
		}
		if((mat.GetInt("_boolDay") % 2 == 1)&&(Color.white != mat.GetColor("_NightColor")))
		{
			if(mat.GetColor("_NightColor").r <=1)
			{
				Color tmp = mat.GetColor("_NightColor");
				tmp = new Color(tmp.r + Time.deltaTime,tmp.r + Time.deltaTime,tmp.r + Time.deltaTime);
				if(tmp.r >= upperLimit)
					tmp = new Color(upperLimit,upperLimit,upperLimit,1);
				mat.SetColor("_NightColor",tmp);
			}
		}else if((mat.GetInt("_boolDay") % 2 == 0)&&(originCol != mat.GetColor("_NightColor")))
		{
			Color tmp = mat.GetColor("_NightColor");
			tmp = new Color(tmp.r - Time.deltaTime,tmp.r - Time.deltaTime,tmp.r - Time.deltaTime);
			if(tmp.r <= originCol.r)
				tmp = originCol;
			mat.SetColor("_NightColor",tmp);
		}
	}
}
