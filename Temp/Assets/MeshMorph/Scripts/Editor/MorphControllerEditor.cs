using UnityEngine;
using UnityEditor;


namespace MeshMorpher
{
	[CustomEditor(typeof(MorphController))]
	public class MorphControllerEditor : Editor
	{
		private MorphController morphController;

		private void OnEnable()
		{
			morphController = (MorphController)target;
			morphController.UpdateMeshes();
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Morph Contoller Setup", EditorStyles.boldLabel);

			if (DrawDefaultInspector())
			{
				morphController.UpdateMorphController();
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Information", EditorStyles.boldLabel);

			EditorGUILayout.LabelField(morphController.GetModelsData(), GUILayout.MaxHeight(40));

		}

	}

	
}

