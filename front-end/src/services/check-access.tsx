'use server';

import {User} from "@/types";

export async function checkAccess(requiredRoles: string[] = [], user: User) {
    try {
        console.log(user);
        return !(requiredRoles.length > 0 && !requiredRoles.some(role => user.roles?.includes(role)));


    } catch (error) {
        console.error(error);
    }
}
