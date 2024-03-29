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
import { BoundMemberRoleData } from './bound-member-role-data.model';
import { DiscordRoleInfo } from './discord-role-info.model';


export interface MemberSummaryData { 
    userId?: number;
    dashboardId?: string;
    name?: string | null;
    discriminator?: string | null;
    discordId?: number;
    avatar?: string | null;
    joinedAt?: string;
    discordRoles?: Array<DiscordRoleInfo> | null;
    roles?: Array<BoundMemberRoleData> | null;
}

