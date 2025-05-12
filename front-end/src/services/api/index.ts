export * from './shared';
import {  AuthApi } from './auth/auth.api';
import { CisternsApi } from './cisterns/cisterns.api';
import { VesselsApi } from './cisterns/vessels.api';
import { FilesApi } from './files/files.api';
import { LocationsApi } from './locations/locations.api';
import { ManufacturersApi } from './manufacturers/manufacturers.api';
import { InstallationsApi } from './parts/installations.api';
import { PartsApi } from './parts/parts.api';
import { WheelPairsApi } from './parts/parts-types/wheel-pairs.api';
import { RegistrarsApi } from './registrars/registrars.api';
import { WagonTypesApi } from './wagon-types/wagon-types.api';
// Импортируйте другие API модули

const ApiService = {
    auth: AuthApi,
    cisterns: CisternsApi,
    vessels: VesselsApi,
    files: FilesApi,
    locations: LocationsApi,
    manufacturers: ManufacturersApi,
    installations: InstallationsApi,
    parts: PartsApi,
    partsTypes: {
        wheelPairs: WheelPairsApi
    },
    registrars: RegistrarsApi,
    wagonTypes: WagonTypesApi
    // Добавьте другие API
};

export { ApiService };
export default ApiService;
