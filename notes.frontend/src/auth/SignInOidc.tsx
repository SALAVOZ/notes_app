import { useNavigate  } from "react-router-dom";
import {FC, useEffect} from "react";
import { signinRedirectCallback } from './user-service';

const SignInOidc: FC<{}> = () => {
    const navigate  = useNavigate();
    useEffect(() => {
        async function signInAsync() {
            await signinRedirectCallback();
            navigate('/')
        }
        signInAsync();
    }, [navigate]);
    return <div>Redirecting...</div>;
};

export default SignInOidc;
