'use client';

import {
    Dialog,
    DialogContent,
    DialogTitle,
    IconButton,
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components";
import React from "react";
import {IconUpload} from "@tabler/icons-react";
import {UploadModal} from "./upload-modal";
import {useFileModal} from "@/stores/useUploadModal";
import {VisuallyHidden} from "@radix-ui/react-visually-hidden";
import {useAuthStore} from "@/stores/useAuth";
import ApiService from "@/services/api";

const UploadButtons: React.FC = () => {
    const fileModal = useFileModal();
    const {user} = useAuthStore();

    const uploadFile = async (file: File) => {
        if (user) {
            await ApiService.files.upload(user.id, file);
        }
    };

    const buttonsData = [
        {
            key: 'file',
            icon: <IconUpload />,
            tooltipText: 'Загрузить файл',
            modal: fileModal,
            content: (
                <UploadModal
                    onClose={fileModal.onClose}
                    onAdd={uploadFile}
                />
            ),
        }
    ];

    return (
        <div className='flex gap-3'>
            <TooltipProvider disableHoverableContent>
                {buttonsData.map((button) => (
                    <React.Fragment key={button.key}>
                        <Tooltip>
                            <TooltipTrigger asChild>
                                <IconButton
                                    onClick={button.modal.onOpen}
                                    ripple
                                >
                                    {button.icon}
                                </IconButton>
                            </TooltipTrigger>
                            <TooltipContent>
                                {button.tooltipText}
                            </TooltipContent>
                        </Tooltip>
                        <Dialog
                            open={button.modal.isOpen}
                            onOpenChange={button.modal.onClose}
                        >
                            <VisuallyHidden>
                                <DialogTitle/>
                            </VisuallyHidden>
                            <DialogContent
                                aria-describedby={undefined}
                            >
                                {button.content}
                            </DialogContent>
                        </Dialog>
                    </React.Fragment>
                ))}
            </TooltipProvider>
        </div>
    );
}

export {UploadButtons};
export default UploadButtons;
