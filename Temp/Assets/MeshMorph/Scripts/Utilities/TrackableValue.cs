namespace MeshMorpher
{
	public struct TrackableValue
	{
		private float currentValue;

		public TrackableValue(float value)
		{
			currentValue = value;
		}

		/// <summary>
		/// Set current value of trackable value, return true if value changed
		/// </summary>
		/// <param name="newValue"></param>
		/// <returns></returns>
		public bool SetCurrentValue(float newValue)
		{
			bool result = currentValue != newValue;
			currentValue = newValue;

			return result;
		}

		public float GetCurrentValue()
		{
			return currentValue;
		}
	}
}


