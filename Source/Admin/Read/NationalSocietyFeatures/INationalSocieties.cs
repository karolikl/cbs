/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017-2018 The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Read.NationalSocietyFeatures
{
    public interface INationalSocieties
    {
        IEnumerable<NationalSociety> GetAll();

        void Save(NationalSociety nationalSociety);
        NationalSociety GetById(Guid id);
    }
}