using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Rule;
using XmasEngineModel.World;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineModel.EntityLib
{
  
	///<summary>
	///     An Entity meant to be added to a XmasWorld
	///</summary>
	public abstract class XmasEntity : XmasUniversal
	{
        private bool loaded = false;
		
		

        internal void Load()
        {
            if (loaded == true)
                return;

            loaded = true;
            OnLoad();
        }


		/// <summary>
		/// This method is called when the entity is first loaded into the engine, will only be called once.
		/// </summary>
        protected internal virtual void OnLoad()
        {
			

        }

		/// <summary>
		/// This method is called every time the entity enters the world
		/// </summary>
        protected internal virtual void OnEnterWorld()
        {

        }

		/// <summary>
		/// This method is called every time the entity leaves the world
		/// </summary>
        protected internal virtual void OnLeaveWorld()
        {

        }

		
		

		/// <summary>
		/// Gets the position of the entity, this is done through the world the entity is located in.
		/// </summary>
		public XmasPosition Position
		{
			get { return World.GetEntityPosition(this); }
		}


        /// <summary>
        /// Gets the position as a certain type
        /// </summary>
        /// <typeparam name="TXmasPosition">The type the position is converted to</typeparam>
        /// <returns></returns>
        public TXmasPosition PositionAs<TXmasPosition>() where TXmasPosition : XmasPosition
        {
            return (TXmasPosition)Position;
        }
		

		

		/// <summary>
		/// Queue an action meant to be performed by the entity onto the entity. This method is threadsafe.
		/// </summary>
		/// <param name="action">The action that is queued</param>
		public void QueueAction(EntityXmasAction action)
		{
			action.Source = this;
			ActionManager.QueueAction(action);
		}

		

		public override string ToString()
		{
			string basestr = string.Format("{0} [{1}]", GetType().Name, Id);
			try
			{
				return string.Format("{0} at {1}", basestr, Position);
			}
			catch
			{
				return basestr;
			}
		}
	}
}