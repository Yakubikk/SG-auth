import React from "react";
import {FileDropzone} from "./file-dropzone";
import {IconButton} from "@/components";
import {IconUpload, IconX} from "@tabler/icons-react";

interface UploadModalProps {
    onClose: () => void;
    onAdd: (file: File) => void;
}

const UploadModal: React.FC<UploadModalProps> = ({ onClose, onAdd }) => {
    return (
        <div className='w-[calc(100vw-12rem)] max-w-6xl min-w-[596px] h-[calc(100vh-10rem)] max-h-[50rem] flex flex-col'>
            <div className='w-full h-fit flex justify-between items-center pl-4 pr-2 py-1 border-b border-gray-300'>
                <IconUpload />
                <IconButton variant='text' size='medium-small' onClick={onClose}>
                    <IconX/>
                </IconButton>
            </div>
            <FileDropzone onAdd={onAdd} onClose={onClose} />
        </div>
    );
}

export {UploadModal};
export default UploadModal;
