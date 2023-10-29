export interface RegisterResponse {
    succeeded: boolean;
    errors: {
        code: string;
        description: string;
    }[];
}