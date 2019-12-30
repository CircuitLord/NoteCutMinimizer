using Harmony;
using UnityEngine;

namespace NoteCutMinimizer
{


	[HarmonyPatch(typeof(NoteDebris))]
	[HarmonyPatch("Init")]
	class NoteDebrisPatch
	{

		public static void Prefix(ref Transform initTransform, ref Vector3 force, ref float lifeTime) {



			if (!NCMSettings.config.enabled) return;

			float minForce = 2f;
			float maxForce = 8f;

			float forceMultiplier = NCMSettings.config.forceMultiplier;

			minForce *= forceMultiplier;
			maxForce *= forceMultiplier;

			
			force.y = Random.Range(-2f * forceMultiplier, -0.15f * forceMultiplier);
			
			
			var note = initTransform.parent.GetComponent<NoteController>();

			if (note) {

				switch (note.noteData.cutDirection) {
					case NoteCutDirection.Left:
						force.x = Random.Range(-maxForce, -minForce);
						break;
					
					case NoteCutDirection.Right:
						force.x = Random.Range(minForce, maxForce);
						break;
					
					case NoteCutDirection.DownLeft:
					case NoteCutDirection.UpLeft:
						force.x = Random.Range(-maxForce / 2, -minForce / 2);
						break;
					
					case NoteCutDirection.DownRight:
					case NoteCutDirection.UpRight:
						force.x = Random.Range(minForce / 2, maxForce / 2);
						break;
				}
				
			}
			

			lifeTime = lifeTime - Mathf.Clamp(initTransform.position.y - 1f, 0f, 0.5f);


			
			



		}




	}
	
	
	
}
