﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CombatCarsAPI.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="fauser7_combatcars")]
	public partial class CombatCarsAPIModelDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertVehicle(Vehicle instance);
    partial void UpdateVehicle(Vehicle instance);
    partial void DeleteVehicle(Vehicle instance);
    partial void InsertUserVehicle(UserVehicle instance);
    partial void UpdateUserVehicle(UserVehicle instance);
    partial void DeleteUserVehicle(UserVehicle instance);
    #endregion
		
		public CombatCarsAPIModelDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["fauser7_combatcarsConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CombatCarsAPIModelDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CombatCarsAPIModelDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CombatCarsAPIModelDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CombatCarsAPIModelDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<Vehicle> Vehicles
		{
			get
			{
				return this.GetTable<Vehicle>();
			}
		}
		
		public System.Data.Linq.Table<UserVehicle> UserVehicles
		{
			get
			{
				return this.GetTable<UserVehicle>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="fauser7_combatcars.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private string _Username;
		
		private string _Password;
		
		private EntitySet<UserVehicle> _UserVehicles;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    #endregion
		
		public User()
		{
			this._UserVehicles = new EntitySet<UserVehicle>(new Action<UserVehicle>(this.attach_UserVehicles), new Action<UserVehicle>(this.detach_UserVehicles));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserVehicle", Storage="_UserVehicles", ThisKey="UserId", OtherKey="UserId")]
		public EntitySet<UserVehicle> UserVehicles
		{
			get
			{
				return this._UserVehicles;
			}
			set
			{
				this._UserVehicles.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_UserVehicles(UserVehicle entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_UserVehicles(UserVehicle entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="fauser7_combatcars.Vehicle")]
	public partial class Vehicle : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _VehicleId;
		
		private string _Name;
		
		private EntitySet<UserVehicle> _UserVehicles;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnVehicleIdChanging(int value);
    partial void OnVehicleIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Vehicle()
		{
			this._UserVehicles = new EntitySet<UserVehicle>(new Action<UserVehicle>(this.attach_UserVehicles), new Action<UserVehicle>(this.detach_UserVehicles));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VehicleId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int VehicleId
		{
			get
			{
				return this._VehicleId;
			}
			set
			{
				if ((this._VehicleId != value))
				{
					this.OnVehicleIdChanging(value);
					this.SendPropertyChanging();
					this._VehicleId = value;
					this.SendPropertyChanged("VehicleId");
					this.OnVehicleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Vehicle_UserVehicle", Storage="_UserVehicles", ThisKey="VehicleId", OtherKey="VehicleId")]
		public EntitySet<UserVehicle> UserVehicles
		{
			get
			{
				return this._UserVehicles;
			}
			set
			{
				this._UserVehicles.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_UserVehicles(UserVehicle entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = this;
		}
		
		private void detach_UserVehicles(UserVehicle entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="fauser7_combatcars.UserVehicle")]
	public partial class UserVehicle : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private int _VehicleId;
		
		private EntityRef<User> _User;
		
		private EntityRef<Vehicle> _Vehicle;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnVehicleIdChanging(int value);
    partial void OnVehicleIdChanged();
    #endregion
		
		public UserVehicle()
		{
			this._User = default(EntityRef<User>);
			this._Vehicle = default(EntityRef<Vehicle>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VehicleId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int VehicleId
		{
			get
			{
				return this._VehicleId;
			}
			set
			{
				if ((this._VehicleId != value))
				{
					if (this._Vehicle.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVehicleIdChanging(value);
					this.SendPropertyChanging();
					this._VehicleId = value;
					this.SendPropertyChanged("VehicleId");
					this.OnVehicleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserVehicle", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.UserVehicles.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.UserVehicles.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Vehicle_UserVehicle", Storage="_Vehicle", ThisKey="VehicleId", OtherKey="VehicleId", IsForeignKey=true)]
		public Vehicle Vehicle
		{
			get
			{
				return this._Vehicle.Entity;
			}
			set
			{
				Vehicle previousValue = this._Vehicle.Entity;
				if (((previousValue != value) 
							|| (this._Vehicle.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Vehicle.Entity = null;
						previousValue.UserVehicles.Remove(this);
					}
					this._Vehicle.Entity = value;
					if ((value != null))
					{
						value.UserVehicles.Add(this);
						this._VehicleId = value.VehicleId;
					}
					else
					{
						this._VehicleId = default(int);
					}
					this.SendPropertyChanged("Vehicle");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
