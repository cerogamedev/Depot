using UnityEngine;
using System.Collections;

namespace Pathfinding
{

	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class NpcAI : VersionedMonoBehaviour
	{
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;
		public LayerMask obstacleLayer;
		public float _speed;

		private SpriteRenderer spr;
		public GameObject product, unfullyDepot, inDepotProd, inPeronProd, unfullyPerron;
		public bool goingProduct = true, goingDepot = false, goingPeron = false, goingProdDepot = true;
		private NpcStamina _stamina;
		void OnEnable()
		{
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}
		private void Start()
		{
			spr = GetComponent<SpriteRenderer>();
			_speed = 2;
			_stamina = this.GetComponent<NpcStamina>();
		}

		void OnDisable()
		{
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update()
		{
			if (target != null && ai != null) ai.destination = target.position;
			this.GetComponent<AIPath>().maxSpeed = _speed;
			FindProduct();
			FindUnfullyDepot();
			FindInDepotProd();
			FindUnfullyPerron();

			Flip();
			AILogic();
			StaminaLogic();
		}

		private void StaminaLogic()
        {

			if (unfullyPerron != null)
			{
				if (target == unfullyPerron.transform)
				{
					_stamina.SetStamina(-5 * Time.deltaTime * (1 / _speed));
				}
			}

			if (unfullyDepot!=null)
            {
				if (target == unfullyDepot.transform)
                {
					_stamina.SetStamina(-5 * Time.deltaTime * (1 / _speed));
				}
			}
			if (_stamina.GetStamina()<=0)
            {
				_speed = .50f;
            }
        }

		private void AILogic()
        {
			if (this.gameObject.tag == "IncomePersonal")
            {
				IncomePersonel();
            }
			else if (this.gameObject.tag == "OutcomePersonel")
            {
				OutcomePersonel();
            }
        }
		private void IncomePersonel()
        {
			if (goingProduct && product != null)
            {
				target = product.transform;
				Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.y);
				Vector2 prodPos = new Vector2(product.transform.position.x, product.transform.position.y);
				float distance = Vector2.Distance(thisPos, prodPos);
				if (distance < 0.3f)
				{
					product.transform.SetParent(this.gameObject.transform);
					_speed = product.GetComponent<Product>()._productHeavy;
					product.transform.tag = "goingProd";
					product = null;
					goingProduct = false;
					goingDepot = true;
				}
			}
			if (product == null || unfullyDepot == null)
            {
				GameObject waitingArea = GameObject.Find("WaitingArea");
				target = waitingArea.transform;
            }
			if (goingDepot && unfullyDepot != null)
            {

				target = unfullyDepot.transform;

				Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.y);
				Vector2 depotPos = new Vector2(unfullyDepot.transform.position.x, unfullyDepot.transform.position.y);
				float distance0 = Vector2.Distance(thisPos, depotPos);

				GameObject depotProd = null;
				GameObject[] allBulwarks = GameObject.FindGameObjectsWithTag("goingProd");
				float distanceToClosestBulwark = Mathf.Infinity;
				foreach (GameObject currentBulwark in allBulwarks)
				{
					float distanceToBulwark = (currentBulwark.transform.position - this.transform.position).sqrMagnitude;
					if (distanceToBulwark < distanceToClosestBulwark)
					{
						distanceToClosestBulwark = distanceToBulwark;
						depotProd = currentBulwark;
					}
				}
				if (distance0 <= 0.4f)
				{

					depotProd.transform.tag = "DepotProd";
					depotProd.transform.SetParent(unfullyDepot.transform);
					_speed = 2;
					depotProd = null;
					goingProduct = true;
					goingDepot = false;
				}
			}
        }
		private void OutcomePersonel()
		{
			//go to depotprod
			if (goingProdDepot && inDepotProd != null)
            {
				target = inDepotProd.transform;
				Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.y);
				Vector2 inDepotPos = new Vector2(inDepotProd.transform.position.x, inDepotProd.transform.position.y);
				float distance1 = Vector2.Distance(thisPos, inDepotPos);

				if (distance1 <= 0.4f)
				{
					_speed = inDepotProd.GetComponent<Product>()._productHeavy;
					inDepotProd.transform.tag = "goingProd";
					inDepotProd.transform.SetParent(this.transform);
					inDepotProd = null;
					goingProdDepot = false;
					goingPeron = true;
				}
			}

			if (inDepotProd == null || unfullyPerron == null)
			{
				GameObject waitingArea = GameObject.Find("WaitingArea");
				target = waitingArea.transform;
			}
			//go to unfullyPerron
			if (goingPeron && unfullyPerron != null)
			{
				target = unfullyPerron.transform;
				Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.y);
				Vector2 inDepotPos = new Vector2(unfullyPerron.transform.position.x, unfullyPerron.transform.position.y);
				float distance2 = Vector2.Distance(thisPos, inDepotPos);

				if (distance2 <= 0.4f)
				{
					
					this.transform.GetChild(1).transform.tag = "PerronProd";
					this.transform.GetChild(1).transform.SetParent(unfullyPerron.transform);
					_speed = 2;
					unfullyPerron = null;
					goingProdDepot = true;
					goingPeron = false;
				}
			}

		}

		private void Flip()
		{
			if (target != null)
			{
				Vector2 direction = (target.position - transform.position).normalized;
				spr.flipX = direction.x < 0;

			}
		}
		public void FindProduct()
		{
			GameObject[] allBulwarks = GameObject.FindGameObjectsWithTag("Product");
			float distanceToClosestBulwark = Mathf.Infinity;
			foreach (GameObject currentBulwark in allBulwarks)
			{
				float distanceToBulwark = (currentBulwark.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToBulwark < distanceToClosestBulwark)
				{
					distanceToClosestBulwark = distanceToBulwark;
					product = currentBulwark;
				}
			}
		}
		public void FindUnfullyDepot()
		{
			GameObject[] allBulwarks = GameObject.FindGameObjectsWithTag("Unfully Depot");
			float distanceToClosestBulwark = Mathf.Infinity;
			foreach (GameObject currentBulwark in allBulwarks)
			{
				float distanceToBulwark = (currentBulwark.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToBulwark < distanceToClosestBulwark)
				{
					distanceToClosestBulwark = distanceToBulwark;
					unfullyDepot = currentBulwark;
				}
			}
			if (unfullyDepot != null)
				if (unfullyDepot.tag != "Unfully Depot")
					unfullyDepot = null;
		}
		public void FindInDepotProd()
		{
			GameObject[] allBulwarks = GameObject.FindGameObjectsWithTag("DepotProd");
			float distanceToClosestBulwark = Mathf.Infinity;
			foreach (GameObject currentBulwark in allBulwarks)
			{
				float distanceToBulwark = (currentBulwark.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToBulwark < distanceToClosestBulwark)
				{
					distanceToClosestBulwark = distanceToBulwark;
					inDepotProd = currentBulwark;
				}
			}
			if (inDepotProd != null)
				if (inDepotProd.tag != "DepotProd")
					inDepotProd = null;
		}
		public void FindUnfullyPerron()
		{
			GameObject[] allBulwarks = GameObject.FindGameObjectsWithTag("UnfullPerron");
			float distanceToClosestBulwark = Mathf.Infinity;
			foreach (GameObject currentBulwark in allBulwarks)
			{
				float distanceToBulwark = (currentBulwark.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToBulwark < distanceToClosestBulwark)
				{
					distanceToClosestBulwark = distanceToBulwark;
					unfullyPerron = currentBulwark;
				}
			}
			if (unfullyPerron != null)
				if (unfullyPerron.tag != "UnfullPerron")
					unfullyPerron = null;
		}
	}
}