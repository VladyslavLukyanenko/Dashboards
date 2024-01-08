/**
 * Dashboards API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { HostingConfig } from './hosting-config.model';
import { DiscordConfig } from './discord-config.model';
import { StripeIntegrationConfig } from './stripe-integration-config.model';


export interface DashboardData { 
    id?: string;
    name?: string | null;
    stripeConfig?: StripeIntegrationConfig;
    expiresAt?: string;
    discordConfig?: DiscordConfig;
    logoSrc?: string | null;
    customBackgroundSrc?: string | null;
    timeZoneId?: string | null;
    hostingConfig?: HostingConfig;
    chargeBackersExportEnabled?: boolean;
}
