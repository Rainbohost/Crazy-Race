

using UnityEngine;
using UnityEditor;
using System.Collections;

public class RCC_EditorWindows : Editor {

	#region Edit Settings
	[MenuItem("Tools/X-Zone Games/Car Controller/Edit RCC Settings", false, -100)]
	public static void OpenRCCSettings(){
		Selection.activeObject =RCC_Settings.Instance;
	}
	#endregion

	#region Configure
	[MenuItem("Tools/X-Zone Games/Car Controller/Configure Ground Materials", false, -65)]
	public static void OpenGroundMaterialsSettings(){
		Selection.activeObject =RCC_GroundMaterials.Instance;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Configure Changable Wheels", false, -65)]
	public static void OpenChangableWheelSettings(){
		Selection.activeObject = RCC_ChangableWheels.Instance;
	}
	#endregion

	#region Add Cameras
	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Cameras/Add RCC Camera To Scene", false, -50)]
	public static void CreateRCCCamera(){

		if (GameObject.FindObjectOfType<RCC_Camera> ()) {

			EditorUtility.DisplayDialog ("Scene has RCC Camera already!", "Scene has RCC Camera already!", "Ok");
			Selection.activeGameObject = GameObject.FindObjectOfType<RCC_Camera> ().gameObject;

		} else {

			GameObject cam = Instantiate (RCC_Settings.Instance.RCCMainCamera.gameObject);
			cam.name = RCC_Settings.Instance.RCCMainCamera.name;
			Selection.activeGameObject = cam.gameObject;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Cameras/Add Hood Camera To Vehicle", false, -50)]
	public static void CreateHoodCamera(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			if(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().gameObject.GetComponentInChildren<RCC_HoodCamera>()){
				EditorUtility.DisplayDialog("Your Vehicle Has Hood Camera Already!", "Your vehicle has hood camera already!", "Ok");
				Selection.activeGameObject = Selection.activeGameObject.GetComponentInChildren<RCC_HoodCamera>().gameObject;
				return;
			}

			GameObject hoodCam = (GameObject)Instantiate(RCC_Settings.Instance.hoodCamera, Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.position, Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.rotation);
			hoodCam.name = RCC_Settings.Instance.hoodCamera.name;
			hoodCam.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.transform, true);
			hoodCam.GetComponent<ConfigurableJoint>().connectedBody = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().gameObject.GetComponent<Rigidbody>();
			RCC_LabelEditor.SetIcon(hoodCam, RCC_LabelEditor.LabelIcon.Purple);
			Selection.activeGameObject = hoodCam;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Cameras/Add Hood Camera To Vehicle", true)]
	public static bool CheckCreateHoodCamera() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Cameras/Add Wheel Camera To Vehicle", false, -50)]
	public static void CreateWheelCamera(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			if(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().gameObject.GetComponentInChildren<RCC_WheelCamera>()){
				EditorUtility.DisplayDialog("Your Vehicle Has Wheel Camera Already!", "Your vehicle has wheel camera already!", "Ok");
				Selection.activeGameObject = Selection.activeGameObject.GetComponentInChildren<RCC_WheelCamera>().gameObject;
				return;
			}

			GameObject wheelCam = new GameObject("Wheel Camera");
			wheelCam.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform, false);
			wheelCam.AddComponent<RCC_WheelCamera>();
			RCC_LabelEditor.SetIcon(wheelCam, RCC_LabelEditor.LabelIcon.Purple);
			Selection.activeGameObject = wheelCam;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Cameras/Add Wheel Camera To Vehicle", true)]
	public static bool CheckCreateWheelCamera() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}
	#endregion

	#region Add Lights
	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/HeadLight", false, -50)]
	public static void CreateHeadLight(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			GameObject lightsMain;

			if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights")){
				lightsMain = new GameObject("Lights");
				lightsMain.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.transform, false);
			}else{
				lightsMain = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights").gameObject;
			}

			GameObject headLight = GameObject.Instantiate (RCC_Settings.Instance.headLights, lightsMain.transform.position, lightsMain.transform.rotation) as GameObject;
			headLight.name = RCC_Settings.Instance.headLights.name;
			headLight.transform.SetParent(lightsMain.transform);
			headLight.transform.localRotation = Quaternion.identity;
			headLight.transform.localPosition = new Vector3(0f, 0f, 2f);
			RCC_LabelEditor.SetIcon(headLight, RCC_LabelEditor.Icon.CircleTeal);
			Selection.activeGameObject = headLight;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/HeadLight", true)]
	public static bool CheckHeadLight() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/Brake", false, -50)]
	public static void CreateBrakeLight(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			GameObject lightsMain;

			if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights")){
				lightsMain = new GameObject("Lights");
				lightsMain.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.transform, false);
			}else{
				lightsMain = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights").gameObject;
			}

			GameObject brakeLight = GameObject.Instantiate (RCC_Settings.Instance.brakeLights, lightsMain.transform.position, lightsMain.transform.rotation) as GameObject;
			brakeLight.name = RCC_Settings.Instance.brakeLights.name;
			brakeLight.transform.SetParent(lightsMain.transform);
			brakeLight.transform.localRotation = Quaternion.identity;
			brakeLight.transform.localPosition = new Vector3(0f, 0f, -2f);
			RCC_LabelEditor.SetIcon(brakeLight, RCC_LabelEditor.Icon.CircleRed);
			Selection.activeGameObject = brakeLight;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/Brake", true)]
	public static bool CheckBrakeLight() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/Reverse", false, -50)]
	public static void CreateReverseLight(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			GameObject lightsMain;

			if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights")){
				lightsMain = new GameObject("Lights");
				lightsMain.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.transform, false);
			}else{
				lightsMain = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights").gameObject;
			}

			GameObject reverseLight = GameObject.Instantiate (RCC_Settings.Instance.reverseLights, lightsMain.transform.position, lightsMain.transform.rotation) as GameObject;
			reverseLight.name = RCC_Settings.Instance.reverseLights.name;
			reverseLight.transform.SetParent(lightsMain.transform);
			reverseLight.transform.localRotation = Quaternion.identity;
			reverseLight.transform.localPosition = new Vector3(0f, 0f, -2f);
			RCC_LabelEditor.SetIcon(reverseLight, RCC_LabelEditor.Icon.CircleGray);
			Selection.activeGameObject = reverseLight;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/Reverse", true)]
	public static bool CheckReverseLight() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/Indicator", false, -50)]
	public static void CreateIndicatorLight(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			GameObject lightsMain;

			if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights")){
				lightsMain = new GameObject("Lights");
				lightsMain.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.transform, false);
			}else{
				lightsMain = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Lights").gameObject;
			}

			GameObject indicatorLight = GameObject.Instantiate (RCC_Settings.Instance.indicatorLights, lightsMain.transform.position, lightsMain.transform.rotation) as GameObject;
			indicatorLight.name = RCC_Settings.Instance.indicatorLights.name;
			indicatorLight.transform.SetParent(lightsMain.transform);
			indicatorLight.transform.localRotation = Quaternion.identity;
			indicatorLight.transform.localPosition = new Vector3(0f, 0f, -2f);
			RCC_LabelEditor.SetIcon(indicatorLight, RCC_LabelEditor.Icon.CircleOrange);
			Selection.activeGameObject = indicatorLight;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Lights/Add Lights To Vehicle/Indicator", true)]
	public static bool CheckIndicatorLight() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}
	#endregion

	#region Add UI
	[MenuItem("Tools/X-Zone Games/Car Controller/Create/UI/Add RCC Canvas To Scene", false, -50)]
	public static void CreateRCCCanvas(){

		if (GameObject.FindObjectOfType<RCC_DashboardInputs> ()) {

			EditorUtility.DisplayDialog ("Scene has RCC Canvas already!", "Scene has RCC Canvas already!", "Ok");
			Selection.activeGameObject = GameObject.FindObjectOfType<RCC_DashboardInputs> ().gameObject;

		} else {

			GameObject canvas = Instantiate (RCC_Settings.Instance.RCCCanvas);
			canvas.name = RCC_Settings.Instance.RCCCanvas.name;

		}

	}
	#endregion

	#region Add Exhausts
	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Add Exhaust To Vehicle", false, -50)]
	public static void CreateExhaust(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{

			GameObject exhaustsMain;

			if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Exhausts")){
				exhaustsMain = new GameObject("Exhausts");
				exhaustsMain.transform.SetParent(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.transform, false);
			}else{
				exhaustsMain = Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.Find(Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().chassis.name+"/Exhausts").gameObject;
			}

			GameObject exhaust = (GameObject)Instantiate(RCC_Settings.Instance.exhaustGas, Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.position, Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().transform.rotation * Quaternion.Euler(0f, 180f, 0f));
			exhaust.name = RCC_Settings.Instance.exhaustGas.name;
			exhaust.transform.SetParent(exhaustsMain.transform);
			exhaust.transform.localPosition = new Vector3(1f, 0f, -2f);
			RCC_LabelEditor.SetIcon(exhaust, RCC_LabelEditor.Icon.DiamondGray);
			Selection.activeGameObject = exhaust;

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Add Exhaust To Vehicle", true)]
	public static bool CheckCreateExhaust() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}
	#endregion

	#region Add Mirrors
	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Add Mirrors To Vehicle", false, -50)]
	static void CreateBehavior(){

		if(!Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>()){

			EditorUtility.DisplayDialog("Select a vehicle controlled by Realistic Car Controller!", "Select a vehicle controlled by Realistic Car Controller!", "Ok");

		}else{
			
			CreateMirrors( Selection.activeGameObject.GetComponentInParent<RCC_CarControllerV3>().gameObject);

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Add Mirrors To Vehicle", true)]
	static bool CheckCreateBehavior() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}
	#endregion

	#region Add Enter-Exit
	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Enter-Exit/Add Enter-Exit Script to Vehicle", false, -50)]
	static void CreateEnterExitVehicleBehavior(){

		GameObject[] selectedGameObjects = Selection.gameObjects;

		for(int i = 0; i < selectedGameObjects.Length; i++){

			if(!selectedGameObjects[i].GetComponent<RCC_EnterExitCar>() && selectedGameObjects[i].GetComponent<RCC_CarControllerV3>()){
				selectedGameObjects[i].AddComponent<RCC_EnterExitCar>();
			}else if(selectedGameObjects[i].GetComponent<RCC_CarControllerV3>()){	
				EditorUtility.DisplayDialog("Your Vehicle Already Has Enter-Exit Script", "Your Vehicle Named " + "''" + selectedGameObjects[i].name + "''"  + " Already Has Enter-Exit Script", "Ok");
			}else if(!selectedGameObjects[i].GetComponent<RCC_CarControllerV3>()){
				EditorUtility.DisplayDialog("Your Vehicle Has Not RCC_CarControllerV3", "Your Vehicle Named " + "''" + selectedGameObjects[i].name + "''"  + " Has Not RCC_CarControllerV3.", "Ok");
			}

		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Enter-Exit/Add Enter-Exit Script to Vehicle", true)]
	static bool CheckEnterExitVehicleBehavior() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Enter-Exit/Add Enter-Exit Script to FPS Player", false, -50)]
	static void CreateEnterExitPlayerBehavior(){

		GameObject selectedGameObject = Selection.activeGameObject;

		if(!selectedGameObject.GetComponentInChildren<RCC_EnterExitPlayer>()){
			if(selectedGameObject.GetComponentInChildren<Camera>() == null){
				EditorUtility.DisplayDialog("Your Player Named " +  "''" + selectedGameObject.name + "''" + " Has Not Any Camera", "Your Player Has Not Any Camera", "Ok");
				return;
			}
			Camera cam = selectedGameObject.GetComponentInChildren<Camera>();
			if (cam.gameObject.GetComponent<RCC_EnterExitPlayer> ()) {
				EditorUtility.DisplayDialog ("Your Player Already Has Enter-Exit Script", "Your Player Named " + "''" + selectedGameObject.name + "''" + " Already Has Enter-Exit Script", "Ok");
			} else {
				cam.gameObject.AddComponent<RCC_EnterExitPlayer> ();
			}
		}else{
			EditorUtility.DisplayDialog("Your Player Already Has Enter-Exit Script", "Your Player Named " + "''" + selectedGameObject.name + "''" + " Already Has Enter-Exit Script", "Ok");
		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Enter-Exit/Add Enter-Exit Script to FPS Player", true)]
	static bool CheckEnterExitPlayerBehavior() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Enter-Exit/Add Enter-Exit Script to TPS Player", false, -50)]
	static void CreateEnterExitTPSPlayerBehavior(){

		GameObject selectedGameObject = Selection.activeGameObject;

		if(!selectedGameObject.GetComponentInChildren<RCC_EnterExitPlayer>()){

			selectedGameObject.AddComponent<RCC_EnterExitPlayer>();
			selectedGameObject.GetComponent<RCC_EnterExitPlayer>().playerType = RCC_EnterExitPlayer.PlayerType.TPS;

		}else{
			EditorUtility.DisplayDialog("Your Player Already Has Enter-Exit Script", "Your Player Named " + "''" + selectedGameObject.name + "''" + " Already Has Enter-Exit Script", "Ok");
		}

	}

	[MenuItem("Tools/X-Zone Games/Car Controller/Create/Misc/Enter-Exit/Add Enter-Exit Script to TPS Player", true)]
	static bool CheckEnterExitTPSPlayerBehavior() {
		if(Selection.gameObjects.Length > 1 || !Selection.activeTransform)
			return false;
		else
			return true;
	}
	#endregion

	#region Help




	#endregion Help

	#region Static Methods
	static void CreateMirrors (GameObject car) {

		if(!car.transform.GetComponentInChildren<RCC_Mirror>()){

			GameObject mirrors = (GameObject)Instantiate(RCC_Settings.Instance.mirrors, car.transform.position, car.transform.rotation);
			mirrors.transform.SetParent(car.GetComponent<RCC_CarControllerV3>().chassis.transform, true);
			mirrors.name = "Mirrors";
			RCC_LabelEditor.SetIcon(mirrors.transform.GetChild(0).gameObject, RCC_LabelEditor.Icon.DiamondRed);
			RCC_LabelEditor.SetIcon(mirrors.transform.GetChild(1).gameObject, RCC_LabelEditor.Icon.DiamondBlue);
			RCC_LabelEditor.SetIcon(mirrors.transform.GetChild(2).gameObject, RCC_LabelEditor.Icon.DiamondTeal);
			Selection.activeGameObject = mirrors;
			EditorUtility.DisplayDialog("Created Mirrors!", "Created mirrors. Adjust their positions.", "Ok");

		}else{

			EditorUtility.DisplayDialog("Vehicle Has Mirrors Already", "Vehicle has mirrors already!", "Ok");

		}

	}
	#endregion

}
