import "./loading.css";
import React from "react";

const Loading: React.FC = () => {

    return (
        <div className="flex flex-col items-center justify-center bg-white">
            <div className="loading-spinner"/>
            <p className="loading-text">{'loading'}</p>
        </div>
    );
}

export { Loading };
export default Loading;
