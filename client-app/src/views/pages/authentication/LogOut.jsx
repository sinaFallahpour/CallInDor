import React, { useEffect } from 'react';
import auth from '../../../core/services/userService/authService';

const Logout = () => {
    useEffect(() => {
        auth.logout();
        window.location = "/pages/login";
    }, []);

    return null;
};

export default Logout;