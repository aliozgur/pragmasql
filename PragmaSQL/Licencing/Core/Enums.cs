/************************************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserver
 * Copyright PragmaSQL 2007 
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Licencing
{
  public enum Product
  {
    PragmaSQL,
    None
  }

  public enum ProductCodeName
  {
    Tulip,
    Rose,
    Carnation,
    Snowdrop,
    Daisy,
    Hyacinthl,
    None
  }

  public enum PurchaseType
  {
    Demo,
    Purchase,
    Promotion,
    None
  }

  public enum LicType
  {
    Machine,
    Server,
    None
  }

	public enum MachineIdType
	{
		Simple,
		Composite,
    Composite2
  }

  public enum PragmaLicElements
  {
    PragmaLicense,
    Product,
    ProductCodeName,
    ActivationKey,
    LicType,
    PurchaseType,
    ValidFrom,
    ValidTo,
    MachineKey,
    EMail,
		MachineIdType
  }
}
