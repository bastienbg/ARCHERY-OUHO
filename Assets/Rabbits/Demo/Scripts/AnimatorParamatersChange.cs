using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiveRabbitsDemo
{
    public class AnimatorParametersChange : MonoBehaviour
    {
        private Animator m_animator;
        private Vector3 m_targetPosition;
        private float m_speed = 2f; // Vitesse de déplacement
        private bool m_isDead = false;
        private float m_pauseDuration = 1f; // Durée de pause entre les mouvements

        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();
            m_animator.SetBool("isRunning", true); // Commencer par l'animation de course
            StartCoroutine(MoveAround()); // Commencer la coroutine pour se déplacer
            StartCoroutine(HandleDeath()); // Commencer la coroutine pour gérer la mort
        }

        private IEnumerator MoveAround()
        {
            while (!m_isDead)
            {
                SetNewTargetPosition();

                // Se déplacer vers la nouvelle cible
                while (Vector3.Distance(transform.position, m_targetPosition) > 0.1f && !m_isDead)
                {
                    MoveToTarget();
                    yield return null; // Attendre la prochaine frame
                }

                // Pause avant de changer de direction
                yield return new WaitForSeconds(m_pauseDuration);
            }
        }

        private void MoveToTarget()
        {
            // Déplacer le lapin vers la cible
            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, m_speed * Time.deltaTime);
            
            // Faire tourner le lapin vers la direction du mouvement
            if (m_targetPosition != transform.position)
            {
                Vector3 direction = (m_targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        private void SetNewTargetPosition()
        {
            // Choisir une nouvelle position aléatoire dans un certain rayon
            m_targetPosition = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
        }

        private IEnumerator HandleDeath()
        {
            // Attendre un certain temps avant de déclencher la mort
            yield return new WaitForSeconds(10f); // Délai avant la mort

            m_isDead = true;
            m_animator.SetBool("isDead", true);
            m_animator.SetBool("isRunning", false);
        }
    }
}
