import { cva, type VariantProps } from 'class-variance-authority';

export enum TextFieldColors {
  primary = 'primary',
  secondary = 'secondary',
}

export enum TextFieldSizes {
  small = 'small',
  medium = 'medium',
  large = 'large',
}

export enum TextFieldVariants {
  standard = 'standard',
  outlined = 'outlined',
}

const textFieldVariants = cva(
  'relative block w-full border-0 border-gray-400 focus:border-gray-800 bg-transparent text-bodyLarge font-medium outline outline-0 transition-colors placeholder:text-black/45 focus:outline-0 disabled:cursor-not-allowed disabled:bg-transparent disabled:text-black/45',
  {
    variants: {
      variant: {
        [`${TextFieldVariants.standard}`]: '',
        [`${TextFieldVariants.outlined}`]: 'px-4',
      },
      size: {
        [`${TextFieldSizes.small}`]: 'h-10 text-body2',
        [`${TextFieldSizes.medium}`]: 'h-12',
        [`${TextFieldSizes.large}`]: 'h-14',
      },
    },
    defaultVariants: {
      size: `${TextFieldSizes.medium}`,
    },
    compoundVariants: [
      {
        variant: `${TextFieldVariants.outlined}`,
        size: `${TextFieldSizes.small}`,
        className: 'px-2.5',
      },
    ],
  }
);

const labelVariants = cva('relative block', {
  variants: {
    color: {
      [`${TextFieldColors.primary}`]: '',
      [`${TextFieldColors.secondary}`]: '',
    },
    size: {
      [`${TextFieldSizes.small}`]: 'text-caption1',
      [`${TextFieldSizes.medium}`]: 'text-body2',
      [`${TextFieldSizes.large}`]: 'text-body2',
    },
  },
  defaultVariants: {
    size: `${TextFieldSizes.medium}`,
  },
  compoundVariants: [],
});

const borderVariants = cva(
  'pointer-events-none absolute inset-0 select-none border-0 border-gray-400 peer-focus:border-black peer-[:not([disabled])]:peer-hover:border-gray-600 peer-data-[error=true]:border-red-500 peer-data-[error=true]:peer-hover:border-red-500',
  {
    variants: {
      variant: {
        [`${TextFieldVariants.standard}`]: 'border-b',
        [`${TextFieldVariants.outlined}`]:
          'rounded-lg border peer-focus:ring-4 peer-focus:ring-blue-500/20 peer-data-[error=true]:peer-focus:ring-red-500/20',
      },
    },
    defaultVariants: {
      variant: `${TextFieldVariants.standard}`,
    },
    compoundVariants: [],
  }
);

export type TextFieldVariantProps = VariantProps<typeof textFieldVariants>;

export { textFieldVariants, labelVariants, borderVariants };
