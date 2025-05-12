import "./loading.css";
import React from "react";

const Loading: React.FC = () => {
    return (
        <div className="w-full h-screen fixed flex flex-col items-center justify-center bg-white">
            <div className="loading-spinner"/>
            <p className="loading-text">Загрузка, пожалуйста, подождите...</p>
        </div>
    );
}

export { Loading };
export default Loading;
