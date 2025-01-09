import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideNgxStripe } from 'ngx-stripe';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes), 
    provideHttpClient(),
    provideNgxStripe('pk_test_51QJzkGRqNFmfQiA9hwu67vH1c9KBiT08qdh3ffTQarVGTvRgwL5w6biM71SPNQcInumTXHZsVifVmDWo4A0USgyL00DSFkGb4m')
  ]
};
