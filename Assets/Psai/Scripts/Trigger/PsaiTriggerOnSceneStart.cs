//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using psai.net;

public class PsaiTriggerOnSceneStart : PsaiTriggerOnSignal
{

    new void Update()
    {
        if (PsaiCore.IsInstanceInitialized())
        {
            SoundtrackInfo soundtrackinfo = PsaiCore.Instance.GetSoundtrackInfo();

            if (soundtrackinfo.themeCount > 0)
            {
                TryToFireSynchronizedShotTrigger();
                this.enabled = false;
            }            
        }        
    }

}
