using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(Date))]
public class DateDrawer : PropertyDrawer
{
	readonly float fieldHeight = 17, fieldWidth = 50, padding = 5, labelWidth = 40;
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		Rect dayLabelRect = new Rect(position.x, position.y, labelWidth, fieldHeight);
		Rect dayRect = new Rect(position.x + labelWidth, position.y, fieldWidth, fieldHeight);

		Rect monthLabelRect = new Rect(position.x + labelWidth + padding + fieldWidth, position.y, labelWidth + 10, fieldHeight);
		Rect monthRect = new Rect(position.x + fieldWidth + padding + (labelWidth * 2) + 10, position.y, fieldWidth, fieldHeight);

		EditorGUI.LabelField(dayLabelRect, GUIContent.none, new GUIContent("Day:"));
		EditorGUI.PropertyField(dayRect, property.FindPropertyRelative("day"),GUIContent.none);

		EditorGUI.LabelField(monthLabelRect, GUIContent.none, new GUIContent("Month:"));
		EditorGUI.PropertyField(monthRect, property.FindPropertyRelative("month"), GUIContent.none);

		//EditorGUI.LabelField(dayLabelRect, GUIContent.none, new GUIContent("D:"));
		//EditorGUI.IntField(dayRect, GUIContent.none, property.FindPropertyRelative("day"));

		//EditorGUI.LabelField(monthLabelRect, GUIContent.none, new GUIContent("M:"));
		//EditorGUI.IntField(monthRect, GUIContent.none, property.FindPropertyRelative("month"));

		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}
