using UnityEngine;
using System.Collections;
using System.Linq;

namespace Util
{
    public static class Common
    {

        public static Vector2 AngleToVector(float angle)
        {
            Vector2 output;
            float radians = angle * Mathf.Deg2Rad;

            output = new Vector2((float)Mathf.Cos(radians), (float)Mathf.Sin(radians));
            return output;
        }

        public static float VectorToAngle(Vector2 v2)
        {
            return Mathf.Atan2(v2.y, v2.x) * 180f / Mathf.PI;
        }

        public static int toInt(this bool b)
        {
            if (b)
                return 1;
            return 0;
        }

        public static float v(this Vector2 v2)
        {
            return Mathf.Sqrt(Mathf.Pow(v2.x, 2) + Mathf.Pow(v2.y, 2));
        }

        public static Bounds getChildBounds(this Transform t)
        {
            Bounds bounds;
            // First find a center for your bounds.
            Vector3 center = Vector3.zero;
            foreach (Transform child in t.transform)
            {
                center += child.gameObject.GetComponent<SpriteRenderer>().bounds.center;
            }
            center /= t.transform.childCount; //center is average center of children

            //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
            bounds = new Bounds(center, Vector3.zero);

            foreach (Transform child in t.transform)
            {
                bounds.Encapsulate(child.gameObject.GetComponent<SpriteRenderer>().bounds);
            }
            return bounds;
        }

        public static Bounds getChildBounds(this Transform t, string ignorNameTag)
        {
            Bounds bounds;

            //so i don't do useless checks
            if (ignorNameTag == "")
            {
                // First find a center for your bounds.
                Vector3 center = Vector3.zero;
                foreach (Transform child in t.transform)
                {
                    center += child.gameObject.GetComponent<SpriteRenderer>().bounds.center;
                }
                center /= t.transform.childCount; //center is average center of children

                //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
                bounds = new Bounds(center, Vector3.zero);

                foreach (Transform child in t.transform)
                {
                    bounds.Encapsulate(child.gameObject.GetComponent<SpriteRenderer>().bounds);
                }
            }
            //
            else
            {
                ignorNameTag = ignorNameTag.ToLower();
                // First find a center for your bounds.
                Vector3 center = Vector3.zero;
                int i = 0;
                foreach (Transform child in t.transform)
                {
                    if (!child.gameObject.name.ToLower().Contains(ignorNameTag))
                    {
                        //Debug.Log(child.name);
                        center += child.gameObject.GetComponent<SpriteRenderer>().bounds.center;
                        i++;
                    }
                }
                center /= i; //center is average center of children

                //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
                bounds = new Bounds(center, Vector3.zero);

                foreach (Transform child in t.transform)
                {
                    if (!child.name.ToLower().Contains(ignorNameTag))
                        bounds.Encapsulate(child.gameObject.GetComponent<SpriteRenderer>().bounds);
                }
            }
            return bounds;
        }

        public static Bounds getChildBounds(this Transform t, string[] IgnorNameTags)
        {
            if (IgnorNameTags.Length == 0)
                return t.getChildBounds();

            Bounds bounds;
            Vector3 center = Vector3.zero;
            int i = 0;
            string n;


            for (int j = 0; j < IgnorNameTags.Length; j++)
            {
                IgnorNameTags[j] = IgnorNameTags[j].ToLower();
            }

            // First find a center for your bounds.s
            foreach (Transform child in t.transform)
            {
                n = child.gameObject.name.ToLower();
                if (IgnorNameTags.Any(str => n.Contains(str)))
                {
                    //Debug.Log(child.name);
                    center += child.gameObject.GetComponent<SpriteRenderer>().bounds.center;
                    i++;
                }
            }
            center /= i; //center is average center of children

            //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
            bounds = new Bounds(center, Vector3.zero);

            foreach (Transform child in t.transform)
            {
                n = child.gameObject.name.ToLower();
                if (IgnorNameTags.Any(str => n.Contains(str)))
                    bounds.Encapsulate(child.gameObject.GetComponent<SpriteRenderer>().bounds);
            }

            return bounds;
        }

        public static void DrawBounds(Bounds b)
        {
            Debug.DrawLine(b.max, new Vector3(b.max.x, b.min.y));
            Debug.DrawLine(new Vector3(b.max.x, b.min.y), b.min);
            Debug.DrawLine(b.min, new Vector3(b.min.x, b.max.y));
            Debug.DrawLine(new Vector3(b.min.x, b.max.y), b.max);
        }

        public static float CalculateJumpVerticalSpeed(float targetJumpHeight)
        {
            // From the jump height and gravity we deduce the upwards speed 
            // for the character to reach at the apex.
            Debug.Log(2f * targetJumpHeight * Physics2D.gravity.y);
            return Mathf.Sqrt(2f * targetJumpHeight * Physics2D.gravity.y);
        }

        /// <summary>
        /// breaks up a number into pieces and return the value at the point of the power
        /// </summary>
        /// <param name="number">should not be seen. The number to exstract from</param>
        /// <param name="power">wat what point do i need to take the return number. In powers of 10( 3, 2 ,1 ,0 )</param>
        /// <returns>a number from 0-9</returns>
        public static int getNumbAt(this int number, int power)
        {
            return number / (int)Mathf.Pow(10, power) % 10;
        }
    }
}